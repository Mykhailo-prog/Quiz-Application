namespace QuizProject.Models
{
    public class UserStatistic
    {
        public int Id { get; set; }
        public int TestTried { get; set; }
        public string Time { get; set; } 
        public int Result { get; set; }
        public int TriesCount { get; set; } = 0;
        public int UserId { get; set; }
    }
}
