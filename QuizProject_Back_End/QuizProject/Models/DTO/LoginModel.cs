using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
