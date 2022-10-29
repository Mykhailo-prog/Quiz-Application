using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Services.RepositoryService.RepositoryAbstractions;
using QuizProject.Services.TestLogic;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Razor.Language;
using QuizProject.Models.DTO;

namespace QuizProject.Services.RepositoryService.Repositories
{
    public class UserStatisticRepository : UserStatisticRepositoryAbstraction<UserStatistic, TestLogicContainer<FinishTestResponse>>
    {
        private readonly IStatisticRepositiry<TestStatistic, TestStatisticDTO> _repository;
        public UserStatisticRepository(QuizContext context, StatisticRepository stat) : base(context)
        {
            _repository = stat;
        }

        public override async Task<UserManagerResponse> Create(TestLogicContainer<FinishTestResponse> item)
        {
            try
            {
                var userStat = new UserStatistic
                {
                    QuizUserId = item.User.Id,
                    TestId = item.Test.TestId,
                    Time = item.Result.Time,
                    Result = item.Result.Result,
                    TriesCount = 1
                };

                await _dbSet.AddAsync(userStat);
                Save();


                return await _repository.Update(userStat, item);
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Creating user statistic proccess failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public override bool IsExists(TestLogicContainer<FinishTestResponse> item, int id)
        {
            var result = _dbSet.Where(s => s.QuizUserId == item.User.Id).Any(s => s.TestId == id);

            if(result) return true;

            return false;
        }

        public override async Task<UserManagerResponse> Update(TestLogicContainer<FinishTestResponse> item)
        {
            try
            {
                var userStat = await _dbSet.FirstOrDefaultAsync(s => s.QuizUserId == item.User.Id);

                userStat.Time = item.Result.Time;
                userStat.Result = item.Result.Result;
                userStat.TriesCount++;

                Save();

                return await _repository.Update(userStat, item);
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Creating user statistic proccess failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }
    }
}
