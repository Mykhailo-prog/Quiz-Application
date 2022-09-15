using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<UserCreatedTest> UserCreatedTest { get; set; }
        public virtual TestStatistic TestStatistic { get; set; }
    }
}
