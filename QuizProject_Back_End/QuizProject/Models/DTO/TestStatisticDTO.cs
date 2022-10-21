using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class TestStatisticDTO
    {
        [Required]
        public int TestId { get; set; }
    }
}
