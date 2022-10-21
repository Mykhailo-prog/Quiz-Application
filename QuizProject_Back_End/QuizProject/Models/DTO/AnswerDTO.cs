using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class AnswerDTO
    {
        //TODO: this attribute will check befor method enter [Required]. Same for other models
        //DONE
        [Required]
        public string Answer { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }
}
