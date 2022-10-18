using System.ComponentModel.DataAnnotations.Schema;

namespace QuizProject.Models
{
    public class TestStatistic
    {
        public int Id { get; set; }
        public string BestTime { get; set; }
        public string BestTimeUser { get; set; }
        public int? BestResult { get; set; } = 0;
        public string BestResultUser { get; set; }
        public int AvgTryCount { get; set; } = 0;
        public int? AvgFirstTryResult { get; set; } = 0;
        public int? MinTries { get; set; } = 0;
        public string MinTriesUser { get; set; }
        public int? CountOfAllTries { get; set; } = 0;
        public int TestId { get; set; }
    }
}
