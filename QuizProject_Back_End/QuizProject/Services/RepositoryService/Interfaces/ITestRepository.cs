using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    public interface ITestRepository<T, K> : IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(int id,K item);
        public Task<UserManagerResponse> Update(int id, K item);
    }
}
