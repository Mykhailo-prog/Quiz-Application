using System.ComponentModel.DataAnnotations;

namespace QuizProject.Models.DTO
{
    public class AnswerDTO
    {
        //TODO: this attribute will check befor method enter [Required]. Same for other models
        public string Answer { get; set; }
        public int QuestionId { get; set; }
    }
}
