using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs6065_Homework2.Models
{
    public class FantasyRoster
    {
        [Key]
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        [ForeignKey("Quarterback")]
        public Guid QuarterbackId { get; set; }
        public NflPlayerQuarterback Quarterback { get; set; }

        [ForeignKey("RunningBack1")]
        public Guid RunningBack1Id { get; set; }
        public NflPlayerRunningBack RunningBack1 { get; set; }

        [ForeignKey("RunningBack2")]
        public Guid RunningBack2Id { get; set; }
        public NflPlayerRunningBack RunningBack2 { get; set; }

        [ForeignKey("WideReceiver1")]
        public Guid WideReceiver1Id { get; set; }
        public NflPlayerWideReceiver WideReceiver1 { get; set; }

        [ForeignKey("WideReceiver2")]
        public Guid WideReceiver2Id { get; set; }
        public NflPlayerWideReceiver WideReceiver2 { get; set; }

        [ForeignKey("TightEnd")]
        public Guid TightEndId { get; set; }
        public NflPlayerTightEnd TightEnd { get; set; }
    }
}
