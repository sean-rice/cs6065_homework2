using System;
using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cs6065_Homework2.Models.ViewModels
{
    public class BuildFantasyRosterViewModel
    {
        public List<NflPlayerQuarterback> Quarterbacks { get; set; }
        public List<NflPlayerRunningBack> RunningBacks { get; set; }
        public List<NflPlayerTightEnd> TightEnds { get; set; }
        public List<NflPlayerWideReceiver> WideReceivers { get; set; }

        public Guid QuarterbackId { get; set; }
        public Guid RunningBack1Id { get; set; }
        public Guid RunningBack2Id { get; set; }
        public Guid TightEndId { get; set; }
        public Guid WideReceiver1Id { get; set; }
        public Guid WideReceiver2Id { get; set; }
    }
}
