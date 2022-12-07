using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using QuizProject.Services.RepositoryService.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class DefaultRepositoryAbstraction<T, K> : RepositoryAbstraction<T>, IQuestionRepository<T, K>, ITestConnectionRepository<T, K> where T : class
    {
        protected DefaultRepositoryAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<UserManagerResponse> Create(K item);
        public abstract Task<UserManagerResponse> Update(int id, K item);
    }
    
}
