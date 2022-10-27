using QuizProject.Models;
using QuizProject.Models.DTO;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class UserRepositoryAbstraction<T, K> : AdminExtentionAbstraction<T>, IUserRepository<T, K> where T : class
    {
        protected UserRepositoryAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<UserManagerResponse> Create(K item);
        public abstract Task<UserManagerResponse> Delete(string name);
        public abstract Task<UserManagerResponse> UpdateScore(int id, UserUpdateDTO item);
    }
}
