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
                roster =  await _dbContext.FantasyRosters
                    .Where(roster => userId == roster.OwnerId)
                    .SingleAsync();
            }
            catch { /* nothing needed, roster is already null */ }

            return roster;
        }

        public async Task<bool> DeleteRosterForUserIdAsync(Guid userId)
        {
            var roster = await GetRosterForUserIdAsync(userId);
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
