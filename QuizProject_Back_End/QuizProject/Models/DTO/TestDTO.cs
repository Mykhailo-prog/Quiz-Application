using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class TestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public List<Question> Questions { get; set; }
    }
}
