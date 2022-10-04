namespace QuizProject.Models
{
    public class UserStatistic
    {
        public int Id { get; set; }
        public string Time { get; set; } 
        public int Result { get; set; }
        public int TriesCount { get; set; } = 0;
        public int TestId { get; set; }
        public int QuizUserId { get; set; }
    }
}
