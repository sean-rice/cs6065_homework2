#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Cs6065_Homework2.Data;
using Cs6065_Homework2.Models;
using Cs6065_Homework2.Models.ViewModels;

namespace Cs6065_Homework2.Services
{
    public class ScoringService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RosterService _rosterService;

        public const float FANTASY_POINTS_FOR_WIN = 1.0f;
        public const float FANTASY_POINTS_FOR_TIE = 0.5f;
        public const float FANTASY_POINTS_FOR_LOSS = 0.0f;

        public ScoringService(ApplicationDbContext dbContext,
            RosterService rosterService)
        {
            _dbContext = dbContext;
            _rosterService = rosterService;
        }

        public async Task<ScoreboardViewModel> GetScoreboardAsync(IEnumerable<int> weeks)
        {
            // we can't open multiple connections to the database concurrently,
            // which would be necessary if we wanted to process the users in
            // asynchronously because we call GetUserPointsForWeeksAsync. so,
            // we first collect the list of users with rosters into a list,
            // create an empty collection for the scoreboard rows, then finally
            // sequentially iterate through the user list and fetch their
            // points by week (in a blocking manner) and add the new scoreboard
            // row to a list.
            // see https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#avoiding-dbcontext-threading-issues
            var usersWithRosters = await _dbContext.Users
                .Where(u => _dbContext.FantasyRosters.Where(r => r.OwnerId == u.Id).SingleOrDefault() != null)
                .ToListAsync();
            
            List<ScoreboardRow> rows = new List<ScoreboardRow>(usersWithRosters.Count);
            foreach (var user in usersWithRosters)
            {
                var pointsByWeek = GetUserPointsForWeeksAsync(user, weeks)
                    .Result
                    .ToDictionary(item => item.Week, item => item.Points);
                
                rows.Add(new ScoreboardRow
                    {
                        Username = user.UserName,
                        PointsByWeek = pointsByWeek
                    });
            }
            var scoreboard = new ScoreboardViewModel { Rows = rows };
            return scoreboard;
        }

        public async Task<IEnumerable<(int Week, float Points)>> GetUserPointsForWeeksAsync(
            ApplicationUser user, IEnumerable<int> weeks)
        {
            FantasyRoster? roster = await _rosterService.GetRosterForUserIdAsync(user.Id);
            if (roster == null)
            {
                return weeks.Select(week => (week, float.NaN));
            }
            return weeks.Select(week => (week, GetRosterPointsForWeek(roster, week)));
        }

        /*
         * Calculates the sum number of fantasy points a given FantasyRoster
         * earned (for all players, across all games) during a certain week.
         */
        public float GetRosterPointsForWeek(FantasyRoster roster, int week)
        {
            float fantasyScore = roster.AsPlayersEnumerable() // for all players in the roster
                // calculate how many points the individual players earned
                .Select(player => GetFantasyPointsForNflPlayerDuringWeek(player, week))
                // and add up all the points across all the players
                .Sum();
            return fantasyScore;
        }

        /*
         * Calculates the sum number of fantasy points a given NflPlayer earned
         * in all games during a certain week.
         */
        private float GetFantasyPointsForNflPlayerDuringWeek(NflPlayer nflPlayer, int week)
        {
            var involvedGames = _dbContext.NflGames
                // find games during that week that the player's team was involved in
                .Where(g => g.Week == week)
                .Where(g => g.PrimaryTeamId == nflPlayer.Team.Id || g.SecondaryTeamId == nflPlayer.Team.Id)
                .ToList(); // return list from database
            var playerScore = involvedGames
                // convert/calculate the fantasy points for that player's team in those games
                .Select(g => GetFantasyPointsForTeamInGame(g, nflPlayer.TeamId))
                // and add up all the points from the games (if multiple).
                .Sum();
            return playerScore;
        }

        /* 
         * Calculates the number of fantasy points an NflTeam with Id teamId
         * earned in a given NflGame according to the rules specified in the
         * assignment instructions.
         */
        private float GetFantasyPointsForTeamInGame(NflGame game, Guid teamId)
        {
            if (game.PrimaryTeamId == teamId)
            {
                if (game.PrimaryPoints > game.SecondaryPoints)
                {
                    return FANTASY_POINTS_FOR_WIN;
                }
                else if (game.PrimaryPoints == game.SecondaryPoints)
                {
                    return FANTASY_POINTS_FOR_TIE;
                }
                else
                {
                    return FANTASY_POINTS_FOR_LOSS;
                }
            }
            else if (game.SecondaryTeamId == teamId)
            {
                if (game.SecondaryPoints > game.PrimaryPoints)
                {
                    return FANTASY_POINTS_FOR_WIN;
                }
                else if (game.SecondaryPoints == game.PrimaryPoints)
                {
                    return FANTASY_POINTS_FOR_TIE;
                }
                else
                {
                    return FANTASY_POINTS_FOR_LOSS;
                }
            }
            return 0.0f; // team was not involved (not primary or secondary)
        }
    }
}
