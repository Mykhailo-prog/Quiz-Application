using System.Collections.Generic;

namespace QuizProject.Models.DTO
{
    public class UserUpdateDTO
    {
        public int Test { get; set; }
        public string Time { get; set; }
        public List<string> userAnswers { get; set; }
    }
}
