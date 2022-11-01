using Microsoft.AspNetCore.Identity;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Models.Entity;
using QuizProject.Services.RepositoryService.Repositories;
using Serilog;
using System;
using System.Linq;

namespace QuizProject.Services.RepositoryService
{
    public class RepositoryFactory
    {
        private readonly QuizContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICalculateStatistic _statistic;

        public RepositoryFactory(QuizContext context, UserManager<IdentityUser> userManager, ICalculateStatistic statistic)
        {
            _context = context;
            _userManager = userManager;
            _statistic = statistic;
        }

        public R GetRepository<R>() where R : class
        {
            var typeParam = typeof(R).GenericTypeArguments.FirstOrDefault();

            if(typeParam == typeof(Test))
            {
                var testrepo = new TestRepository(_context);
                return testrepo as R;
            }

            else if(typeParam == typeof(Answer))
            {
                var ansrepo = new AnswerRepository(_context);
                return ansrepo as R;
            }

            else if(typeParam == typeof(QuizUser))
            {
                var tstat = new StatisticRepository(_context, _statistic);
                var ustat = new UserStatisticRepository(_context, tstat);
                var userrepo = new UserRepository(_context, ustat, _userManager);
                return userrepo as R;
            }

            else if (typeParam == typeof(Question))
            {
                var questrepo = new QuestionRepository(_context);
                return questrepo as R;
            }

            else if (typeParam == typeof(UserCreatedTest))
            {
                var connectrepo = new TestConnectionRepository(_context);
                return connectrepo as R;
            }

            else if (typeParam == typeof(TestStatistic))
            {
                var tstat = new StatisticRepository(_context, _statistic);
                return tstat as R;
            }

            /*else if (typeParam == typeof(UserStatistic))
            {
                var tstat = new StatisticRepository(_context, _statistic);
                var ustat = new UserStatisticRepository(_context,tstat);
                return ustat as R;
            }*/


            throw new Exception("Incorrect type!");
            
        }
    }
}
