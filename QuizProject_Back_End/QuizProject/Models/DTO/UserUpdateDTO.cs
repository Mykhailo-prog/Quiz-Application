using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class UserUpdateDTO
    {
        [Required]
        public int Test { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public List<string> userAnswers { get; set; }
    }
}
