using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using System.Threading.Tasks;

namespace QuizProject.Services.AdministratorService
{
    public interface IAdministratorService
    {
        public Task<UserManagerResponse> ResetScore(string name);
        public Task<UserManagerResponse> ChangePassword(string name, string password);
        public Task<UserManagerResponse> ConfirmEmail(string name);
    }
}
