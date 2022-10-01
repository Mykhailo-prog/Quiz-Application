using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    public class TestLogic
    {
        private readonly QuizContext _db;
        public TestLogic(QuizContext context)
        {
            _db = context;
        }

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
        }
        public static FinishTestResponse GetScore(QuizUser user, UserUpdateDTO userResult, Test test)
        {
            
            var questions = test.Questions.ToList();
            double proventResult = 0;

            for (int i = 0; i< userResult.userAnswers.Count; i++)
            {
                if(userResult.userAnswers[i] == questions[i].CorrectAnswer)
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
                Achievements = null
            };
        }
        public void CreateStatistic()
        {

        }
        public void UpdateStatistic()
        {

        }
        private static int ChangeAllTriesCount(List<UserStatistic> userStat)
        {
            int count = 0;
            foreach (var user in userStat)
            {
                
                count += user.TriesCount;
                

            }
            return count;
        }
        private static void ChangeBestResul(List<UserStatistic> userStat, out int BestRes, out UserStatistic BestUser)
        {
            List<int> res = new List<int>();
            foreach(var user in userStat)
            {
                res.Add(user.Result);
            }
            BestRes = res.Max();
            BestUser = userStat.FirstOrDefault(u => u.Result == res.Max());
            
        }
        private static void ChangeBestTime(List<UserStatistic> userStat, out string BestTime,out UserStatistic BestUser)
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
            var users = db.UserStatistic.Where(u => u.TestTried == id).ToList();
            var currUser = users.Find(u => u.UserId == db.QuizUsers.FirstOrDefault(q => q.Login == login).Id); 
            if(currUser.TriesCount == 1)
            {
                stat.AvgTryCount++;
            }
            
            stat.AvgFirstTryResult = Convert.ToInt32(Math.Round(Convert.ToDecimal(((stat.AvgTryCount * 100) / users.Count()))));

            ChangeBestTime(users, out string BTime, out UserStatistic BTUser);
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
            var users = await db.UserStatistic.Where(u => u.TestTried == id).ToListAsync();

            stat.CountOfAllTries = ChangeAllTriesCount(users);
            if(stat.BestResult != 100)
            {
                ChangeBestResul(users, out int BResult, out UserStatistic BRUser);
                stat.BestResult = BResult;
                var resUser = await db.QuizUsers.FirstOrDefaultAsync(u => u.Id == BRUser.UserId);
                stat.BestResultUser = resUser.Login;
            }
            
            return;
        }
    }
}
