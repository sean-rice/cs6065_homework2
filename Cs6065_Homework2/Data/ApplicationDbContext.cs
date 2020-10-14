using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Cs6065_Homework2.Models;

namespace Cs6065_Homework2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NflGame> NflGames { get; set; }
        public DbSet<NflTeam> NflTeams { get; set; }
        public DbSet<NflPlayerQuarterback> NflQuarterbacks { get; set; }
        public DbSet<NflPlayerRunningBack> NflRunningBacks { get; set; }
        public DbSet<NflPlayerTightEnd> NflTightEnds { get; set; }
        public DbSet<NflPlayerWideReceiver> NflWideReceivers { get; set; }

        public DbSet<FantasyRoster> FantasyRosters { get; set; }
    }
}
