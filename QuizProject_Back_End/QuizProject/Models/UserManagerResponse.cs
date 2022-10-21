using System.Collections;
using System.Collections.Generic;

namespace QuizProject.Models
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public QuizUser User { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
