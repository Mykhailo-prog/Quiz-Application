using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<UserCreatedTest> UserCreatedTest { get; set; }
        public virtual TestStatistic TestStatistic { get; set; }
    }
}
