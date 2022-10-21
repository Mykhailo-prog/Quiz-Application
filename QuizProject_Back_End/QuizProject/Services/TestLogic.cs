using QuizProject.Models.DTO;
using QuizProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace QuizProject.Services
{
    class Time
    {
        public int Min { get; set; }
        public int Sec { get; set; }
        public Time(string stringTime)
        {
            Min = Convert.ToInt32(stringTime.Split(':')[0]);
            Sec = Convert.ToInt32(stringTime.Split(':')[1]);
        }
    }
    public class TestLogic : ITestLogic
    {
        private readonly QuizContext _db;
        private readonly ILogger<TestLogic> _logger;
        public TestLogic(QuizContext context, ILogger<TestLogic> logger)
        {
            _db = context;
            _logger = logger;
        }

        //TODO: looks like redutant method, you can just check if something exists with FirstOrDefaultAsync method, is not it?
        //TODO: free space
        //DONE

        public FinishTestResponse GetScore(QuizUser user, UserUpdateDTO userResult, Test test)
        {
            //TODO: Are you sure you want to get every time all Questions??? What will happened if we will have 1 billion records in Question table?
            //Please remember about IQuerible and IEnumerable
            //Here i get questions of current test
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
        //DONE

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
        //DONE

        public async Task UpdateStatistic(QuizUser user, Test test, FinishTestResponse result)
        {
            var userStat = await _db.UserStatistic.FirstOrDefaultAsync(x => x.QuizUserId == user.Id);

            userStat.Time = result.Time;
            userStat.Result = result.Result;
            userStat.TriesCount++;

            await _db.SaveChangesAsync();

            await ChangeStatistic(test.TestId, user, userStat, result);
        }//TODO: free space. if this method does not planning to be used out of this class it should has private modificator. Please check modificators in your services
        //DONE

        private async Task ChangeStatistic(int id, QuizUser user, UserStatistic currUserStat, FinishTestResponse result)
        {
            try
            {
                var stat = await _db.Statistics.FirstOrDefaultAsync(x => x.TestId == id);
                var userStats = await _db.UserStatistic.Where(u => u.TestId == id).ToListAsync();

                ChangeAllTriesCount(userStats, stat);

                if (stat.BestResult != 100)
                {
                    if (stat.BestResult == 0)
                    {
                        stat.BestResult = currUserStat.Result;
                        stat.BestResultUser = user.Login;
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
            catch(Exception e)
            {
                _logger.LogError("Error during calculating statistic!\n{0}", e.Message);
            }
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
    }
}
