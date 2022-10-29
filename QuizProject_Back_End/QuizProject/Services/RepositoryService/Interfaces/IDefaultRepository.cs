using QuizProject.Models;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Interfaces
{
    public interface IDefaultRepository<T, K> : IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(K item);
        //public Task<UserManagerResponse> Delete(int id);
        public Task<UserManagerResponse> Update(int id, K item);
    }
}
