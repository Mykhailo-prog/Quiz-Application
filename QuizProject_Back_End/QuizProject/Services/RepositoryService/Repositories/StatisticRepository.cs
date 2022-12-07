using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.RepositoryService.RepositoryAbstractions;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Policy;
using QuizProject.Models.Entity;
using QuizProject.Models.ResponseModels;

namespace QuizProject.Services.RepositoryService.Repositories
{
    public class StatisticRepository : StatisticAbstraction<TestStatistic, TestStatisticDTO>
    {
        private readonly ICalculateStatistic _statistic;

        public StatisticRepository(QuizContext context, ICalculateStatistic statistic) : base(context)
        {
            _statistic = statistic;
        }

        public override async Task<UserManagerResponse> Create(TestStatisticDTO item)
        {
            try
            {
                var stat = new TestStatistic
                {
                    TestId = item.TestId,
                };

                await _dbSet.AddAsync(stat);
                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Statistic have been created successfully!"
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Creating statistic proccess failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public override async Task<UserManagerResponse> Update(UserStatistic currUserStat, TestLogicContainer<FinishTestResponse> item)
        {
            var stat = await _dbSet.FirstOrDefaultAsync(x => x.TestId == item.Test.TestId);
            var userStats = await _context.UserStatistic.Where(u => u.TestId == item.Test.TestId).ToListAsync();

            List<UserManagerResponse> responses = new List<UserManagerResponse>
            {
                
                _statistic.ChangeAllTriesCount(stat, userStats),
                _statistic.ChangeBestResult(stat, currUserStat, item.User, item.Result),
                _statistic.ChangeMinTries(stat, currUserStat, item.User),
                _statistic.ChangeBestTime(stat, currUserStat, userStats, item.Result),
                _statistic.ChangeAvrTries(stat, currUserStat, userStats, item.Result)
            };

            foreach (var response in responses)
            {
                if (!response.Success)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = response.Message,
                        Errors = response.Errors
                    };
                }

            }

            return new UserManagerResponse
            {
                Success = true,
                Message = "Statistic has beed calculated successfully",
            };
        }
    }
}
