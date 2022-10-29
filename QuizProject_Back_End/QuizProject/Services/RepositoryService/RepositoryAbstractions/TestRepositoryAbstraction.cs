using QuizProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class TestRepositoryAbstraction<T, K> : RepositoryAbstraction<T>, ITestRepository<T, K> where T : class
    {
        protected TestRepositoryAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<UserManagerResponse> Create(int id, K item);
        public abstract Task<UserManagerResponse> Update(int id, K item);
    }
    
}
