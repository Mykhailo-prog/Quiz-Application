using QuizProject.Models;
using QuizProject.Services.RepositoryService.Interfaces;
using QuizProject.Services.TestLogic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    public interface IStatisticRepositiry<T, K> : IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(K item);
        public Task<UserManagerResponse> Update(UserStatistic currUserStat, TestLogicContainer<FinishTestResponse> container);
    }
}
