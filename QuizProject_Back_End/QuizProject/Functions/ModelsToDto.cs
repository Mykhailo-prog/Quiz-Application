using QuizProject.Models.DTO;
using QuizProject.Models;

namespace QuizProject.Functions
{
    public class ModelsToDto
    {
        public static UserDTO UserToDTO(QuizUser user) => new UserDTO
        {
            Login = user.Login,
        };
        public static TestDTO TestToDTO(Test test) => new TestDTO
        {
            Name = test.Name,
        };
        public static AnswerDTO AnswerToDTO(Answer ans) => new AnswerDTO
        {
            Answer = ans.Ans,
            QuestionId = ans.QuestionId,
        };
        public static QuestionDTO QuestToDTO(Question quest) => new QuestionDTO
        {
            Question = quest.Quest,
            CorrectAnswer = quest.CorrectAnswer,
        };
        public static TestStatisticDTO TestStatToDTO(TestStatistic stat) => new TestStatisticDTO
        {
            TestId = stat.TestId,
        };
        public static CreatedTestDTO CreatedTestToDTO(UserCreatedTest uct) => new CreatedTestDTO
        {
            UserId = uct.UserId,
            TestId = uct.TestId,
        };
    }
}
