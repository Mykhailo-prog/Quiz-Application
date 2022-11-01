namespace QuizProject.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Ans { get; set; }
        public int QuestionId { get; set; }
    }
}
