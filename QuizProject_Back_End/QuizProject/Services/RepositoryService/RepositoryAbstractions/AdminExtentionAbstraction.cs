using QuizProject.Models;
using QuizProject.Services.RepositoryService.Interfaces;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class AdminExtentionAbstraction<T> : RepositoryAbstraction<T>, IAdminExtention where T : class
    {
        protected AdminExtentionAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<UserManagerResponse> ChangePassword(string name, string password);
        public abstract Task<UserManagerResponse> ConfirmEmail(string name);
        public abstract Task<UserManagerResponse> ResetScore(string name);
    }
}
