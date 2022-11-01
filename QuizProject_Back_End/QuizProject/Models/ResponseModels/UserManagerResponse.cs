using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject.Models
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
