using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs6065_Homework2.Models
{
    public sealed class NflPlayerQuarterback : NflPlayer
    {
        public override string Position { get; } = "Quarterback";

        [Required]
        [Display(Name = "Passes Completed", ShortName = "Cmp")]
        public int PassesCompleted { get; set; }
        [Required]
        [Display(Name = "Passes Attempted", ShortName = "Att")]
        public int PassesAttempted { get; set; }
        [Required]
        [Display(Name = "Yards Gained by Passing", ShortName = "Yds")]
        public int PassingYardsGained { get; set; }
        [Required]
        [Display(Name = "Passing Touchdowns", ShortName = "TD")]
        public int PassingTouchdowns { get; set; }
        [Required]
        [Display(Name = "Interceptions Thrown", ShortName = "Int")]
        public int InterceptionsThrown { get; set; }
        [Required]
        [Display(Name = "First Downs Passing", ShortName = "1D")]
        public int FirstDownsPassing { get; set; }

        [NotMapped]
        [Display(Name = "Percentage of Passes Completed", ShortName = "Cmp%")]
        public float PassesCompletionPercentage
        {
            get { return PassesCompleted / PassesAttempted; }
        }
    }
}
