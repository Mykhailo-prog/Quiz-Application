using System.Collections.Generic;

namespace QuizProject.Models.ResponseModels
{
    public class FinishTestResponse
    {
        public string UserName { get; set; }
        public string Time { get; set; }
        public int Result { get; set; }
        public List<string> Achievements { get; set; } = new List<string>();
    }
}
