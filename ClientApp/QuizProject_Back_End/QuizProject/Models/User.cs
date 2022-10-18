using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QuizProject.Models
{
    public class QuizUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int Score { get; set; }
        public ICollection<UserStatistic> UserTestCount { get; set; }
        public ICollection<UserCreatedTest> CreatedTests { get; set; }

    }
}
 