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

        public async Task<IEnumerable<NflPlayerQuarterback>> GetQuarterbacks()
        {
            return await _dbContext.NflQuarterbacks
                .Include(player => player.Team)
                .ToListAsync();
        }
    }
}
