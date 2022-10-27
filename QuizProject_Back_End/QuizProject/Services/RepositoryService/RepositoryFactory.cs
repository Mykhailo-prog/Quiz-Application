using Microsoft.AspNetCore.Identity;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.RepositoryService.Repositories;
using QuizProject.Services.TestLogic;
using Serilog;
using System;
using System.Linq;

namespace QuizProject.Services.RepositoryService
{
    public class RepositoryFactory
    {
        private readonly QuizContext _context;
        private readonly ITestLogic _testLogic;
        private readonly UserManager<IdentityUser> _userManager;
        public RepositoryFactory(QuizContext context, ITestLogic logic, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _testLogic = logic;
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
                var userrepo = new UserRepository(_context, _testLogic, _userManager);
                return userrepo as R;
            }

            throw new Exception("Incorrect type!");
            
        }
    }
}
