#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Cs6065_Homework2.Data;
using Cs6065_Homework2.Models;

namespace Cs6065_Homework2.Services
{
    public class NflPlayerService
    {
        //private readonly ILogger<NflPlayerService> _logger;
        private readonly ApplicationDbContext _dbContext;

        public NflPlayerService(//ILogger<NflPlayerService> logger,
            ApplicationDbContext dbContext)
        {
            //_logger = logger;
            _dbContext = dbContext;
        }

        private async Task<NflPlayerT?> GetPlayerByIdAsync<NflPlayerT>(
            Guid Id, 
            DbSet<NflPlayerT> dbSet) where NflPlayerT : NflPlayer
        {
            NflPlayerT? player = null;
            try
            {
                player = await dbSet
                    .Where(p => p.Id == Id)
                    .Include(p => p.Team)
                    .SingleAsync();
            }
            catch {}
            return player;
        }

        public IQueryable<NflPlayerQuarterback> GetQuarterbacks()
        {
            return _dbContext.NflQuarterbacks
                .Include(player => player.Team);
        }
        public IQueryable<NflPlayerRunningBack> GetRunningBacks()
        {
            return _dbContext.NflRunningBacks
                .Include(player => player.Team);
        }
        public IQueryable<NflPlayerTightEnd> GetTightEnds()
        {
            return _dbContext.NflTightEnds
                .Include(player => player.Team);
        }
        public IQueryable<NflPlayerWideReceiver> GetWideReceivers()
        {
            return _dbContext.NflWideReceivers
                .Include(player => player.Team);
        }

        public async Task<NflPlayerQuarterback?> GetQuarterbackByIdAsync(Guid Id)
        {
            return await GetPlayerByIdAsync<NflPlayerQuarterback>(Id, _dbContext.NflQuarterbacks);
        }
        public async Task<NflPlayerRunningBack?> GetRunningBackByIdAsync(Guid Id)
        {
            return await GetPlayerByIdAsync<NflPlayerRunningBack>(Id, _dbContext.NflRunningBacks);
        }
        public async Task<NflPlayerTightEnd?> GetTightEndByIdAsync(Guid Id)
        {
            return await GetPlayerByIdAsync<NflPlayerTightEnd>(Id, _dbContext.NflTightEnds);
        }
        public async Task<NflPlayerWideReceiver?> GetWideReceiverByIdAsync(Guid Id)
        {
            return await GetPlayerByIdAsync<NflPlayerWideReceiver>(Id, _dbContext.NflWideReceivers);
        }
    }
}
