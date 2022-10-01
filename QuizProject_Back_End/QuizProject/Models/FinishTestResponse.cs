using System.Collections.Generic;

namespace QuizProject.Models
{
    public class FinishTestResponse
    {
        public string UserName { get; set; }
        public string Time { get; set; }
        public int Result { get; set; }
        public IEnumerable<string> Achievements { get; set; }
    }
}
