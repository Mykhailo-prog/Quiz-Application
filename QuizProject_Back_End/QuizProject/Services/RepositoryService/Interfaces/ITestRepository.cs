using QuizProject.Models;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    public interface ITestRepository<T, K> : IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(int id,K item);
        public Task<UserManagerResponse> Delete(int id);
        public Task<UserManagerResponse> Update(int id, K item);
    }
}
