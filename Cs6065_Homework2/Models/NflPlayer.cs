using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs6065_Homework2.Models
{
    public abstract class NflPlayer
    {
        // most of the [Required]s on Nfl* class models-- those on value
        // types-- are not strictly necessary, but i'm adding them to pretty
        // much every member for simplicity.
        
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(32)]
        [Display(Name = "Position", ShortName = "Pos")] // inherited by subclasses
        public abstract string Position { get; }

        [Required]
        [StringLength(32)]
        [Display(Name = "First Name", ShortName = "FName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32)]
        [Display(Name = "Last Name", ShortName = "LName")]
        public string LastName { get; set; }

        [Required]
        [ForeignKey("Team")]
        public Guid TeamId { get; set; }
        public NflTeam Team { get; set; }

        [Required]
        [Display(Name = "Games Played", ShortName = "G")]
        public int GamesPlayed { get; set; }
        [Required]
        [Display(Name = "Games Started", ShortName = "GS")]
        public int GamesStarted { get; set; }
    }
}
