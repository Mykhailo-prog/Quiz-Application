using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Functions
{
    class Time
    {
        public int Min { get; set; }
        public int Sec { get; set; }
    }
    public class Methods
    {
        public static bool ElemExists<T>(int id, QuizContext db)
        {
            string param = typeof(T).Name;
            switch (param)
            {
                case "User":
                    return db.QuizUsers.Any(e => e.Id == id);
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
            CorrectAnswer =quest.CorrectAnswer,
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
        public async static Task<int[]> GetScore(List<string> answers, int testId, int score, QuizContext db)
        {
            int[] res = new int[2];
            double procScore = 0;
            db.Questions.Load();
            var test = await db.Tests.FindAsync(testId);
            var quests = test.Questions.ToList();
            for(int i = 0; i < answers.Count; i++)
            {
                if (answers[i] == quests[i].CorrectAnswer)
                {
                    procScore++;
                    score += 10;
                }
            }
            res[1] = Convert.ToInt32(Math.Round((procScore * 100) / answers.Count));
            res[0] = score;
            return res;
        }
        private static int ChangeAllTriesCount(List<UserTestCount> userStat)
        {
            int count = 0;
            foreach (var user in userStat)
            {
                
                count += user.TriesCount;
                

            }
            return count;
        }
        private static void ChangeBestResul(List<UserTestCount> userStat, out int BestRes, out UserTestCount BestUser)
        {
            List<int> res = new List<int>();
            foreach(var user in userStat)
            {
                res.Add(user.Result);
            }
            BestRes = res.Max();
            BestUser = userStat.FirstOrDefault(u => u.Result == res.Max());
            
        }
        private static void ChangeBestTime(List<UserTestCount> userStat, out string BestTime,out UserTestCount BestUser)
        {
            var ParsedTime = new List<Time>();
            foreach(var user in userStat)
            {
                ParsedTime.Add(new Time { Min = Convert.ToInt32(user.Time.Split(':')[0]),  Sec = Convert.ToInt32(user.Time.Split(':')[1]) });
            }
            var Rtime = ParsedTime.OrderBy(x => x.Sec).OrderBy(x => x.Min);
            string res = $"{Rtime.First().Min}:{Rtime.First().Sec}";
            BestTime = res;
            BestUser = userStat.FirstOrDefault(u => u.Time == res);
        }
        public static async Task ChangeStatistic(int id, string login, int tries, QuizContext db)
        {
            var stat = await db.Statistics.FirstOrDefaultAsync(s => s.TestId == id);
            var users = db.UserTests.Where(u => u.TestTried == id).ToList();
            var currUser = users.Find(u => u.UserId == db.QuizUsers.FirstOrDefault(q => q.Login == login).Id); 
            if(currUser.TriesCount == 1)
            {
                stat.AvgTryCount++;
            }
            
            stat.AvgFirstTryResult = Convert.ToInt32(Math.Round(Convert.ToDecimal(((stat.AvgTryCount * 100) / users.Count()))));

            ChangeBestTime(users, out string BTime, out UserTestCount BTUser);
            stat.BestTime = BTime;
            var timeUser = await db.QuizUsers.FirstOrDefaultAsync(u => u.Id == BTUser.UserId);
            stat.BestTimeUser = timeUser.Login;

            if (stat.MinTries == 0)
            {
                stat.MinTries = tries;
                stat.MinTriesUser = login;
            }
            else
            {
                if (tries < stat.MinTries)
                {
                    stat.MinTries = tries;
                    stat.MinTriesUser = login;
                }
            }
            
            return;
        }
        public static async Task ChangeStatistic(int id, QuizContext db)
        {
            var stat = await db.Statistics.FirstOrDefaultAsync(s => s.TestId == id);
            var users = await db.UserTests.Where(u => u.TestTried == id).ToListAsync();

            stat.CountOfAllTries = ChangeAllTriesCount(users);
            if(stat.BestResult != 100)
            {
                ChangeBestResul(users, out int BResult, out UserTestCount BRUser);
                stat.BestResult = BResult;
                var resUser = await db.QuizUsers.FirstOrDefaultAsync(u => u.Id == BRUser.UserId);
                stat.BestResultUser = resUser.Login;
            }
            
            return;
        }
    }
}
