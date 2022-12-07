using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Models.ResponseModels;
using QuizProject.Services.RepositoryService.Interfaces;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    public interface IUserRepository<T, K> : IRepository<T> where T : class
    {
        public Task<T> GetByName(string name);
        public Task<UserManagerResponse> Create(K item);
        public Task<UserManagerResponse> Delete(string name);
        public Task<UserManagerResponse> UpdateScore(int id, UserUpdateDTO item);
    }
}
