using QuizProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class AnswerRepositoryAbstraction<T, K> : RepositoryAbstraction<T>, IAnswerRepository<T, K> where T : class
    {
        protected AnswerRepositoryAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<UserManagerResponse> Create(List<K> item);
        public abstract Task<UserManagerResponse> Update(int id, K item);
    }

}
