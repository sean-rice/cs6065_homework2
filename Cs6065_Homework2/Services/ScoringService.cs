#nullable enable
using System;
using System.Linq;
using System.Threading.Tasks;

using Cs6065_Homework2.Data;
using Cs6065_Homework2.Models;

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

        public async Task<float> GetUserPointsForWeek(Guid userId, int week)
        {
            FantasyRoster? roster = await _rosterService.GetRosterForUserIdAsync(userId);
            if (roster == null)
            {
                return float.NaN;
            }
            float fantasyScore = roster.AsPlayersEnumerable()
                .Select(p => GetFantasyPointsForPlayerDuringWeek(p, week))
                .Sum();
            return fantasyScore;
        }

        private float GetFantasyPointsForPlayerDuringWeek(NflPlayer player, int week)
        {
            float playerScore = _dbContext.NflGames
                // find games during that week
                .Where(g => g.Week == week)
                // that the player's team was involved in
                .Where(g => g.TeamWasInGame(player.TeamId))
                // convert/calculate the fantasy points for that player's team in those games
                .Select(g => GetFantasyPointsForTeamInGame(g, player.TeamId))
                // and add up all the points from the games (if multiple).
                .Sum();
            return playerScore;
        }

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
