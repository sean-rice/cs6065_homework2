using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs6065_Homework2.Models
{
    public class NflPlayerRunningBack : NflPlayer
    {
        public override string Position { get; } = "Running Back";

        [Required]
        [Display(Name = "Rushing Attempts", ShortName = "Rush")]
        public int RushingAttempts { get; set; }
        [Required]
        [Display(Name = "Rushing Yards Gained", ShortName = "Yds")]
        public int RushingYards { get; set; }
        [Required]
        [Display(Name = "Rushing Touchdowns", ShortName = "TD")]
        public int RushingTouchdowns { get; set; }
        [Required]
        [Display(Name = "First Downs Rushing", ShortName = "1D")]
        public int FirstDownsRushing { get; set; }
        [Required]
        [Display(Name = "Longest Rushing Attempt", ShortName = "Lng")]
        public int LongestRushingAttempt { get; set; }

        [NotMapped]
        [Display(Name = "Rushing Yards per Attempt", ShortName = "Y/A")]
        public float RushingYardsPerAttempt
        {
            get { return RushingYards / RushingAttempts; }
        }

    }
}
