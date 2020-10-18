using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs6065_Homework2.Models
{
    public class NflGame
    {
        // most of the [Required]s on Nfl* class models-- those on value
        // types-- are not strictly necessary, but i'm adding them to pretty
        // much every member for simplicity.
        
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Week { get; set; }
        [Required]
        public int PrimaryPoints { get; set; }
        [Required]
        public int SecondaryPoints { get; set; }


        [ForeignKey("PrimaryTeam")]
        public Guid PrimaryTeamId { get; set; }
        public NflTeam PrimaryTeam { get; set; }

        [ForeignKey("SecondaryTeam")]
        public Guid SecondaryTeamId { get; set; }
        public NflTeam SecondaryTeam { get; set; }

        public bool TeamWasInGame(Guid teamId)
        {
            return teamId == PrimaryTeamId || teamId == SecondaryTeamId;
        }
    }
}
