using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Login { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength (50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
    }
}
