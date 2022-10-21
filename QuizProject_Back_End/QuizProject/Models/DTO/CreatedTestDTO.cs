using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class CreatedTestDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TestId { get; set; }
    }
}
