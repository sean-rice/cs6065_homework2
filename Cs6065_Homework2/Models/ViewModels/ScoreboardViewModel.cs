using System.Collections.Generic;

namespace Cs6065_Homework2.Models.ViewModels
{
    public class ScoreboardRow
    {
        public string Username { get; set; }
        public Dictionary<int, float> PointsByWeek { get; set; }
    }

    public class ScoreboardViewModel
    {
        public IEnumerable<ScoreboardRow> Rows { get; set; }
    }
}
