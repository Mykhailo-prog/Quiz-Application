using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class QuestionDTO
    {
        [Required]
        [MaxLength(50)]
        public string Question { get; set; }
        [Required]
        public string CorrectAnswer { get; set; }
        [Required]
        public int TestId { get; set; }

    }
}
