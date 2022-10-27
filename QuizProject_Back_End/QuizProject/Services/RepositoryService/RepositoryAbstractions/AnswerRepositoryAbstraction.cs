using QuizProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class AnswerRepositoryAbstraction<T, K> : RepositoryAbstraction<T>, IAnswerRepository<T, K> where T : class
    {
        protected AnswerRepositoryAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<UserManagerResponse> Create(List<K> item);
        public virtual async Task<UserManagerResponse> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Delete operation failed!",
                    Errors = new List<string> { "Entity with this id not found" }
                };
            }

            _dbSet.Remove(entity);

            Save();

            return new UserManagerResponse
            {
                Success = true,
                Message = $"Entity deleted successfully!"
            };
        }
        public abstract Task<UserManagerResponse> Update(int id, K item);
    }

}
