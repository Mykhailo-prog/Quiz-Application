using QuizProject.Models.DTO;
using QuizProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizProject.Services.TestLogic;

namespace QuizProject.Services.TestLogic
{
    public class TestLogic : ITestLogic
    {
        private readonly QuizContext _db;
        private readonly ILogger<TestLogic> _logger;
        private readonly ICalculateStatistic _statistic;
        public TestLogic(QuizContext context, ILogger<TestLogic> logger, ICalculateStatistic statistic)
        {
            _db = context;
            _logger = logger;
            _statistic = statistic;
        }

        //TODO: looks like redutant method, you can just check if something exists with FirstOrDefaultAsync method, is not it?
        //TODO: free space
        //DONE

        public UserManagerResponse<FinishTestResponse> GetScore(QuizUser user, UserUpdateDTO userResult, Test test)
        {
            //TODO: Are you sure you want to get every time all Questions??? What will happened if we will have 1 billion records in Question table?
            //Please remember about IQuerible and IEnumerable
            //Here i get questions of current test

            try
            {
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

                var result = new FinishTestResponse
                {
                    UserName = user.Login,
                    Time = userResult.Time,
                    Result = Convert.ToInt32(Math.Round(proventResult * 100 / userResult.userAnswers.Count)),
                };
                return new UserManagerResponse<FinishTestResponse>
                {
                    Success = true,
                    Message = "User score counted successfully!",
                    Object = result
                };
            }
            catch (Exception e)
            {
                _logger.LogError("User score count failed\n{0}", e.Message);

                return new UserManagerResponse<FinishTestResponse>
                {
                    Success = false,
                    Message = "User score count failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }//TODO: free space
        //DONE

        public async Task<UserManagerResponse> ChangeUserStatistic(QuizUser user, Test test, FinishTestResponse result, int id)
        {
            if (!_db.UserStatistic.Where(e => e.QuizUserId == user.Id).Any(u => u.TestId == id))
            {
                return await CreateStatisticAsync(user, test, result);
            }
            else
            {
                var res = await UpdateStatisticAsync(user, test, result);
                return res;
            }
        }

        private async Task<UserManagerResponse> CreateStatisticAsync(QuizUser user, Test test, FinishTestResponse result)
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

            return await _statistic.CalculateStat(test.TestId, user, userStat, result);
        }//TODO: free space
        //DONE

        private async Task<UserManagerResponse> UpdateStatisticAsync(QuizUser user, Test test, FinishTestResponse result)
        {
            var userStat = await _db.UserStatistic.FirstOrDefaultAsync(x => x.QuizUserId == user.Id);

            userStat.Time = result.Time;
            userStat.Result = result.Result;
            userStat.TriesCount++;

            await _db.SaveChangesAsync();
            var res = await _statistic.CalculateStat(test.TestId, user, userStat, result);
            return res;
        }//TODO: free space. if this method does not planning to be used out of this class it should has private modificator. Please check modificators in your services
        //DONE

    }
}
