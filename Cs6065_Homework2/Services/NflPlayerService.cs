using System.Collections.Generic;
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

        public async Task<IEnumerable<NflPlayerQuarterback>> GetQuarterbacksAsync()
        {
            return await _dbContext.NflQuarterbacks
                .Include(player => player.Team)
                .ToListAsync();
        }
        public async Task<IEnumerable<NflPlayerRunningBack>> GetRunningBacksAsync()
        {
            return await _dbContext.NflRunningBacks
                .Include(player => player.Team)
                .ToListAsync();
        }
        public async Task<IEnumerable<NflPlayerTightEnd>> GetTightEndsAsync()
        {
            return await _dbContext.NflTightEnds
                .Include(player => player.Team)
                .ToListAsync();
        }
        public async Task<IEnumerable<NflPlayerWideReceiver>> GetWideReceiversAsync()
        {
            return await _dbContext.NflWideReceivers
                .Include(player => player.Team)
                .ToListAsync();
        }
    }
}
