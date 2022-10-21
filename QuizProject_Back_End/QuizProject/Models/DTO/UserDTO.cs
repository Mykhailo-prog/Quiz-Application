using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class UserDTO
    {
        [Required]
        public string Login { get; set; }

    }
}
