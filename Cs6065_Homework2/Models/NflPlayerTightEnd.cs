using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs6065_Homework2.Models
{
    public class NflPlayerTightEnd : NflPlayer
    {
        public override string Position { get; } = "Tight End";

        [Required]
        [Display(Name = "Pass Targets", ShortName = "Tgt")]
        public int PassTargets { get; set; }
        [Required]
        [Display(Name = "Receptions", ShortName = "Rec")]
        public int Receptions { get; set; }
        [Required]
        [Display(Name = "Receiving Yards", ShortName = "Yds")]
        public int ReceivingYards { get; set; }
        [Required]
        [Display(Name = "First Downs Receiving", ShortName = "1D")]
        public int FirstDownsReceiving { get; set; }
        [Required]
        [Display(Name = "Yards Before Catch", ShortName = "YBC")]
        public int YardsBeforeCatch { get; set; }
        [Required]
        [Display(Name = "Yards After Catch", ShortName = "YAC")]
        public int YardsAfterCatch { get; set; }

        [NotMapped]
        [Display(Name = "Yards Before Catch per Reception", ShortName = "YBC/R")]
        public float YardsBeforeCatchPerReception
        {
            get { return YardsBeforeCatch / Receptions; }
        }

        [NotMapped]
        [Display(Name = "Yards After Catch per Reception", ShortName = "YAC/R")]
        public float YardsAfterCatchPerReception
        {
            get { return YardsAfterCatch / Receptions; }
        }
    }
}
