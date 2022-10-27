using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.RepositoryService.Interfaces;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    public interface IUserRepository<T, K> : IAdminExtention ,IRepository<T> where T : class
    {
        public Task<UserManagerResponse> Create(K item);
        public Task<UserManagerResponse> Delete(string name);
        public Task<UserManagerResponse> UpdateScore(int id, UserUpdateDTO item);
    }
}
