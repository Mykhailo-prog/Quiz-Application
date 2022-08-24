using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Functions
{
    public class Methods
    {
        public static bool ElemExists<T>(int id, QuizContext db)
        {
            string param = typeof(T).Name;
            switch (param)
            {
                case "User":
                    return db.Users.Any(e => e.Id == id);
                case "Answer":
                    return db.Answers.Any(e => e.Id == id);
                case "Question":
                    return db.Questions.Any(e => e.Id == id);
                case "Test":
                    return db.Tests.Any(e => e.TestId == id);
                default:
                    return false;
            }
        }
        public static UserDTO UserToDTO(User user) => new UserDTO
        {
            Login = user.Login,
            Password = user.Password,
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
            CorrectAnswer =quest.CorrectAnswer,
        };
        public static CreatedTestDTO CreatedTestToDTO(UserCreatedTest uct) => new CreatedTestDTO
        {
            UserId = uct.UserId,
            TestId = uct.TestId,
        };
        public async static Task<int> GetScore(List<string> answers, int testId, int score, QuizContext db)
        {
            db.Questions.Load();
            var test = await db.Tests.FindAsync(testId);
            var quests = test.Questions.ToList();
            for(int i = 0; i < answers.Count; i++)
            {
                if (answers[i] == quests[i].CorrectAnswer)
                {
                    score += 10;
                }
            }
            return score;
        }
    }
}
