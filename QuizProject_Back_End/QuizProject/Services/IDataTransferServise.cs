using QuizProject.Models.DTO;
using QuizProject.Models;

namespace QuizProject.Services
{
    public interface IDataTransferServise
    {
        public UserDTO UserToDTO(QuizUser user);
        public TestDTO TestToDTO(Test test);
        public AnswerDTO AnswerToDTO(Answer ans);
        public QuestionDTO QuestToDTO(Question quest);
        public TestStatisticDTO TestStatToDTO(TestStatistic stat);
        public CreatedTestDTO CreatedTestToDTO(UserCreatedTest uct);
    }
}
