#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using CsvHelper;

using Cs6065_Homework2.Models;

namespace Cs6065_Homework2.Data
{
    using csvHeaderValidatedType = Action<bool, string[], int, ReadingContext>;

    public static class SeedDatabase
    {
        private static readonly string ConfigBaseSection = "Initialization:SeedData";

        public static async Task<bool> SeedAll(
            ILogger logger,
            IConfiguration configuration,
            ApplicationDbContext dbContext)
        {
            // note that this initialization order is somewhat important
            // teams referenced by games and players must already exist at
            // their insertion time
            bool nflTeams = await SeedNflTeams(logger, configuration, dbContext);
            bool nflGames = await SeedNflGames(logger, configuration, dbContext);
            bool nflQuarterbacks = await SeedNflQuarterbacks(logger, configuration, dbContext);
            bool nflRunningBacks = await SeedNflRunningBacks(logger, configuration, dbContext);
            bool nflTightEnds = await SeedNflTightEnds(logger, configuration, dbContext);
            bool nflWideReceivers = await SeedNflWideReceivers(logger, configuration, dbContext);
            return nflTeams && nflGames && nflQuarterbacks && nflRunningBacks && nflTightEnds && nflWideReceivers;
        }

        private static void Report(ILogger? logger, string dbSetName, int recordsCount, int recordsWritten)
        {
            if (recordsCount == recordsWritten)
            {
                logger?.LogInformation($"seeded {dbSetName} with all {recordsCount} records.");
            }
            else
            {
                logger?.LogWarning($"found write count mismatch when seeding {dbSetName}: {recordsCount} records, {recordsWritten} reported written.");
            }
        }


        private static async Task<bool> SeedNflTeams(
            ILogger? logger,
            IConfiguration configuration,
            ApplicationDbContext dbContext)
        {
            const string dbSetName = "NflTeams";
            string csvPath = configuration.GetValue<string>(ConfigBaseSection + ":" + dbSetName);
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = new List<NflTeam>(csv.GetRecords<NflTeam>());
            foreach (var record in records)
            {
                dbContext.NflTeams.Add(record);
            }
            try
            {
                int recordsWritten = await dbContext.SaveChangesAsync();
                Report(logger, dbSetName, records.Count, recordsWritten);
                return records.Count == recordsWritten;
            }
            catch (DbUpdateException)
            {
                logger?.LogWarning($"caught DbUpdateException when seeding {dbSetName}.");
                return false;
            }
        }

        private static async Task<bool> SeedNflGames(
            ILogger? logger,
            IConfiguration configuration,
            ApplicationDbContext dbContext)
        {
            const string dbSetName = "NflGames";
            string csvPath = configuration.GetValue<string>(ConfigBaseSection + ":" + dbSetName);
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.HeaderValidated = null;
            csv.Configuration.MissingFieldFound = null;
            var records = new List<NflGame>(csv.GetRecords<NflGame>());
            foreach (var record in records)
            {
                var primaryTeam = await dbContext.NflTeams
                    .Where(team => team.Id == record.PrimaryTeamId)
                    .SingleAsync();
                var secondaryTeam = await dbContext.NflTeams
                    .Where(team => team.Id == record.SecondaryTeamId)
                    .SingleAsync();
                record.PrimaryTeam = primaryTeam;
                record.SecondaryTeam = secondaryTeam;
                dbContext.NflGames.Add(record);
            }
            try
            {
                int recordsWritten = await dbContext.SaveChangesAsync();
                Report(logger, dbSetName, records.Count, recordsWritten);
                return records.Count == recordsWritten;
            }
            catch (DbUpdateException)
            {
                logger?.LogWarning($"caught DbUpdateException when seeding {dbSetName}.");
                return false;
            }
        }

        public static async Task<bool> SeedNflPlayer<NflPlayerT>(
            ILogger? logger,
            IConfiguration configuration,
            ApplicationDbContext dbContext,
            DbSet<NflPlayerT> targetDbSet,
            string targetDbSetName,
            string configFinalSection) where NflPlayerT : NflPlayer
        {
            string csvPath = configuration.GetValue<string>(ConfigBaseSection + ":" + configFinalSection);
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.HeaderValidated = null;
            csv.Configuration.MissingFieldFound = null;
            var records = new List<NflPlayerT>(csv.GetRecords<NflPlayerT>());
            foreach (NflPlayerT record in records)
            {
                var playerTeam = await dbContext.NflTeams
                    .Where(team => team.Id == record.TeamId)
                    .SingleAsync();
                record.Team = playerTeam;
                targetDbSet.Add(record);
            }
            try
            {
                int recordsWritten = await dbContext.SaveChangesAsync();
                Report(logger, targetDbSetName, records.Count, recordsWritten);
                return records.Count == recordsWritten;
            }
            catch (DbUpdateException)
            {
                logger?.LogWarning($"caught DbUpdateException when seeding {targetDbSetName}.");
                return false;
            }
        }

        public static async Task<bool> SeedNflQuarterbacks(
            ILogger logger,
            IConfiguration configuration, 
            ApplicationDbContext dbContext)
        {
            return await SeedNflPlayer<NflPlayerQuarterback>(
                logger, configuration, dbContext, dbContext.NflQuarterbacks,
                "NflQuarterbacks", "NflQuarterbacks"
            );
        }

        public static async Task<bool> SeedNflRunningBacks(
            ILogger logger,
            IConfiguration configuration, 
            ApplicationDbContext dbContext)
        {
            return await SeedNflPlayer<NflPlayerRunningBack>(
                logger, configuration, dbContext, dbContext.NflRunningBacks,
                "NflRunningBacks", "NflRunningBacks"
            );
        }

        public static async Task<bool> SeedNflTightEnds(
            ILogger logger,
            IConfiguration configuration, 
            ApplicationDbContext dbContext)
        {
            return await SeedNflPlayer<NflPlayerTightEnd>(
                logger, configuration, dbContext, dbContext.NflTightEnds, 
                "NflTightEnds", "NflTightEnds"
            );
        }

        public static async Task<bool> SeedNflWideReceivers(
            ILogger logger,
            IConfiguration configuration, 
            ApplicationDbContext dbContext)
        {
            return await SeedNflPlayer<NflPlayerWideReceiver>(
                logger, configuration, dbContext, dbContext.NflWideReceivers,
                "NflWideReceivers", "NflWideReceivers"
            );
        }
    }
}
