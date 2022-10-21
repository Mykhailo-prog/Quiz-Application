using QuizProject.Models.DTO;
using QuizProject.Models;

namespace QuizProject.Services
{
    public class DataTransferService : IDataTransferServise
    {
        public UserDTO UserToDTO(QuizUser user) => new UserDTO
        {
            Login = user.Login,
        };
        public TestDTO TestToDTO(Test test) => new TestDTO
        {
            Name = test.Name,
        };
        public AnswerDTO AnswerToDTO(Answer ans) => new AnswerDTO
        {
            Answer = ans.Ans,
            QuestionId = ans.QuestionId,
        };
        public QuestionDTO QuestToDTO(Question quest) => new QuestionDTO
        {
            Question = quest.Quest,
            CorrectAnswer = quest.CorrectAnswer,
        };
        public TestStatisticDTO TestStatToDTO(TestStatistic stat) => new TestStatisticDTO
        {
            TestId = stat.TestId,
        };
        public CreatedTestDTO CreatedTestToDTO(UserCreatedTest uct) => new CreatedTestDTO
        {
            UserId = uct.QuizUserId,
            TestId = uct.TestId,
        };
    }
}
