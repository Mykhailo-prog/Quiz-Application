using System.Collections.Generic;

namespace QuizProject.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Quest { get; set; }
        public string CorrectAnswer { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public int TestId { get; set; }
        
    }
}
