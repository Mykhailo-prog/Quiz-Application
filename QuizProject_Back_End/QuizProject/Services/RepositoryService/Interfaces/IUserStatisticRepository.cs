using QuizProject.Models;
using QuizProject.Services.TestLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Interfaces
{
    public interface IUserStatisticRepository<T, K> : IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(K item);
        public Task<UserManagerResponse> Update(K item);
        public bool IsExists(TestLogicContainer<FinishTestResponse> item, int id);
    }
}
