#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Cs6065_Homework2.Data;
using Cs6065_Homework2.Models;

namespace Cs6065_Homework2.Services
{
    public class RosterService
    {
        private readonly ApplicationDbContext _dbContext;

        public RosterService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FantasyRoster?> GetRosterForUserIdAsync(Guid userId)
        {
            FantasyRoster? roster = null;
            try
            {
                // eagerly fetch the whole roster, including sub-objects
                // if the Include()s are left out, the member sub-objects will
                // be null! not good for outside consumers who just want a full roster.
                // this is pretty annoying/weird, i wonder if there is a better way...
                roster =  await _dbContext.FantasyRosters
                    .Where(roster => userId == roster.OwnerId)
                    .Include(roster => roster.Owner)
                    .Include(roster => roster.Quarterback)
                        .ThenInclude(player => player.Team)
                    .Include(roster => roster.RunningBack1)
                        .ThenInclude(player => player.Team)
                    .Include(roster => roster.RunningBack2)
                        .ThenInclude(player => player.Team)
                    .Include(roster => roster.TightEnd)
                        .ThenInclude(player => player.Team)
                    .Include(roster => roster.WideReceiver1)
                        .ThenInclude(player => player.Team)
                    .Include(roster => roster.WideReceiver2)
                        .ThenInclude(player => player.Team)
                    .SingleAsync();
            }
            catch { /* nothing needed, roster is already null */ }

            return roster;
        }

        public async Task<bool> AddOrUpdateRosterAsync(FantasyRoster roster)
        {
            try
            {
                FantasyRoster? dbRoster = null;
                dbRoster = _dbContext.FantasyRosters
                    .Where(r => r.OwnerId == roster.OwnerId)
                    .SingleOrDefault();
                if (dbRoster == null)
                {
                    _dbContext.FantasyRosters.Add(roster);
                }
                else
                {
                    dbRoster = roster;
                    _dbContext.FantasyRosters.Update(dbRoster);
                }
                int changes = await _dbContext.SaveChangesAsync();
                return changes > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRosterForUserIdAsync(Guid userId)
        {
            FantasyRoster? roster = await _dbContext.FantasyRosters
                .Where(r => r.OwnerId == userId)
                .SingleOrDefaultAsync();
            if (roster == null)
            {
                return false;
            }

            _dbContext.FantasyRosters.Remove(roster);
            var saveResult = await _dbContext.SaveChangesAsync();

            return saveResult > 0;
        }

    }
}
