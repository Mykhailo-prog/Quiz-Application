using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Interfaces
{
    public interface IDefaultRepository<T, K> : IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(K item);
        public Task<UserManagerResponse> Update(int id, K item);
    }
}
