using QuizProject.Models;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Interfaces
{
    public interface IAdminExtention
    {
        public Task<UserManagerResponse> ResetScore(string name);
        public Task<UserManagerResponse> ChangePassword(string name, string password);
        public Task<UserManagerResponse> ConfirmEmail(string name);
    }
}
