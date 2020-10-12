using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs6065_Homework2.Models
{
    public class NflPlayerWideReceiver : NflPlayer
    {
        public override string Position { get; } = "Wide Receiver";

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
        [Display(Name = "Receiving Touchdowns", ShortName = "TD")]
        public int ReceivingTouchdowns { get; set; }
        [Required]
        [Display(Name = "First Downs Receiving", ShortName = "1D")]
        public int FirstDownsReceiving { get; set; }

        [NotMapped]
        [Display(Name = "Receiving Yards per Reception", ShortName = "Y/R")]
        public float ReceivingYardsPerAttempt
        {
            get { return ReceivingYards / Receptions; }
        }
    }
}
