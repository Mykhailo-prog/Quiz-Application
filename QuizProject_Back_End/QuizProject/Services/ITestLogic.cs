using QuizProject.Models.DTO;
using QuizProject.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

//TODO: every class, interface, enum etc should has separated file
//TODO: Please read about SOLID. If you want to read/write/change/ statistic it should be separated service etc. S - Single Responsibility Principle
namespace QuizProject.Services
{
    public interface ITestLogic
    {
        public bool ElemExists<T>(int id);
        public FinishTestResponse GetScore(QuizUser user, UserUpdateDTO userResult, Test test);
        public Task CreateStatisticAsync(QuizUser user, Test test, FinishTestResponse result);
        public Task UpdateStatistic(QuizUser user, Test test, FinishTestResponse result);
        public UserDTO UserToDTO(QuizUser user);
        public TestDTO TestToDTO(Test test);
        public AnswerDTO AnswerToDTO(Answer ans);
        public QuestionDTO QuestToDTO(Question quest);
        public TestStatisticDTO TestStatToDTO(TestStatistic stat);
        public CreatedTestDTO CreatedTestToDTO(UserCreatedTest uct);

    }
    class Time
    {
        public int Min { get; set; }
        public int Sec { get; set; }
        public Time(string stringTime)
        {
            Min = Convert.ToInt32(stringTime.Split(':')[0]);
            Sec = Convert.ToInt32(stringTime.Split(':')[1]);
        }
    }//TODO: free space
    public class TestLogic : ITestLogic
    {
        
        private readonly QuizContext _db;
        public TestLogic(QuizContext context)
        {
            _db = context;
        }

        //TODO: looks like redutant method, you can just check if something exists with FirstOrDefaultAsync method, is not it?
        public bool ElemExists<T>(int id)
        {
            string param = typeof(T).Name;
            switch (param)
            {
                case "QuizUser":
                    return _db.QuizUsers.Any(e => e.Id == id);
                case "Answer":
                    return _db.Answers.Any(e => e.Id == id);
                case "Question":
                    return _db.Questions.Any(e => e.Id == id);
                case "Test":
                    return _db.Tests.Any(e => e.TestId == id);
                default:
                    return false;
            }
        }//TODO: free space
        public FinishTestResponse GetScore(QuizUser user, UserUpdateDTO userResult, Test test)
        {
            //TODO: Are you sure you want to get every time all Questions??? What will happened if we will have 1 billion records in Question table?
            //Please remember about IQuerible and IEnumerable
            var questions = test.Questions.ToList();
            double proventResult = 0;

            for (int i = 0; i < userResult.userAnswers.Count; i++)
            {
                if (userResult.userAnswers[i] == questions[i].CorrectAnswer)
                {
                    proventResult++;
                    user.Score += 10;
                }
            }

            return new FinishTestResponse
            {
                UserName = user.Login,
                Time = userResult.Time,
                Result = Convert.ToInt32(Math.Round((proventResult * 100) / userResult.userAnswers.Count)),
            };
        }//TODO: free space
        public async Task CreateStatisticAsync(QuizUser user, Test test, FinishTestResponse result)
        {
            var userStat = new UserStatistic
            {
                QuizUserId = user.Id,
                TestId = test.TestId,
                Time = result.Time,
                Result = result.Result,
                TriesCount = 1
            };
            await _db.UserStatistic.AddAsync(userStat);
            await _db.SaveChangesAsync();

            await ChangeStatistic(test.TestId, user, userStat, result);
        }//TODO: free space
        public async Task UpdateStatistic(QuizUser user, Test test, FinishTestResponse result)
        {
            var userStat = await _db.UserStatistic.FirstOrDefaultAsync(x => x.QuizUserId == user.Id);

            userStat.Time = result.Time;
            userStat.Result = result.Result;
            userStat.TriesCount++;

            await _db.SaveChangesAsync();

            await ChangeStatistic(test.TestId, user, userStat, result);
        }//TODO: free space. if this method does not planning to be used out of this class it should has private modificator. Please check modificators in your services
        public async Task ChangeStatistic(int id, QuizUser user, UserStatistic currUserStat, FinishTestResponse result)
        {
            var stat = await _db.Statistics.FirstOrDefaultAsync(x => x.TestId == id);
            var userStats = await _db.UserStatistic.Where(u => u.TestId == id).ToListAsync();

            ChangeAllTriesCount(userStats, stat);

            if (stat.BestResult != 100)
            {
                if (stat.BestResult == 0)
                {
                    stat.BestResult = currUserStat.Result;
                    result.Achievements.Add("You set the best result!");
                }
                else
                {
                    if (currUserStat.Result > stat.BestResult)
                    {
                        stat.BestResult = currUserStat.Result;
                        stat.BestResultUser = user.Login;
                        result.Achievements.Add("You beat the best result!");
                    }
                }

            }

            if (currUserStat.Result == 100)
            {
                result.Achievements.Add("100% Correct!");
                if (currUserStat.TriesCount == 1)
                {
                    stat.AvgTryCount++;
                    result.Achievements.Add("First 100% result!");
                }

                ChangeBestTime(userStats, stat, result);

                if (stat.MinTries == 0)
                {
                    stat.MinTries = currUserStat.TriesCount;
                    stat.MinTriesUser = user.Login;
                }
                else
                {
                    if (currUserStat.TriesCount < stat.MinTries)
                    {
                        stat.MinTries = currUserStat.TriesCount;
                        stat.MinTriesUser = user.Login;
                    }
                }

            }

            double param = (stat.AvgTryCount * 100) / userStats.Count();
            stat.AvgFirstTryResult = Convert.ToInt32(Math.Round(param));
        }

        private static void ChangeAllTriesCount(List<UserStatistic> userStat, TestStatistic stat)
        {
            int count = 0;
            foreach (var user in userStat)
            {

                count += user.TriesCount;


            }
            stat.CountOfAllTries = count;
        }
        private void ChangeBestTime(List<UserStatistic> userStat, TestStatistic stat, FinishTestResponse result)
        {

            var ParsedTime = new List<Time>();
            foreach (var u in userStat)
            {
                ParsedTime.Add(new Time(u.Time));
            }
            var Rtime = ParsedTime.OrderBy(x => x.Sec).OrderBy(x => x.Min);
            string res = $"{Rtime.First().Min}:{Rtime.First().Sec}";
            stat.BestTime = res;
            stat.BestTimeUser = _db.QuizUsers.Find(userStat.FirstOrDefault(u => u.Time == res).QuizUserId).Login;
            if (stat.BestResultUser == result.UserName)
            {
                result.Achievements.Append("You beat a time record!");
            }
        }
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
