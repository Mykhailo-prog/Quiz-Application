using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using QuizProject.Services.RepositoryService.Interfaces;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class UserStatisticRepositoryAbstraction<T, K> : RepositoryAbstraction<T>, IUserStatisticRepository<T, K> where T : class
    {
        public UserStatisticRepositoryAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<UserManagerResponse> Create(K item);
        public abstract bool IsExists(TestLogicContainer<FinishTestResponse> item, int id);
        public abstract Task<UserManagerResponse> Update(K item);
    }
}
