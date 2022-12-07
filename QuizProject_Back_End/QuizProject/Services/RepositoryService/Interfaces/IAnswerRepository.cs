using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    public interface IAnswerRepository<T, K> : IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(List<K> item);
        public Task<UserManagerResponse> Update(int id, K item);
    }
}
