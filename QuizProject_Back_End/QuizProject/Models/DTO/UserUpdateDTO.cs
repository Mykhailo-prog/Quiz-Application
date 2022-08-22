using System.Collections.Generic;

namespace QuizProject.Models.DTO
{
    public class UserUpdateDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
        public int Test { get; set; }
        public List<string> userAnswers { get; set; }
    }
}
