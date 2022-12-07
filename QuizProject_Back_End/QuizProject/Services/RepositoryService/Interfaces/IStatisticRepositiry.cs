using QuizProject.Models;
using QuizProject.Models.Entity;
using QuizProject.Models.ResponseModels;
using QuizProject.Services.RepositoryService.Interfaces;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    public interface IStatisticRepositiry<T, K> : IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(K item);
        public Task<UserManagerResponse> Update(UserStatistic currUserStat, TestLogicContainer<FinishTestResponse> container);
    }
}
