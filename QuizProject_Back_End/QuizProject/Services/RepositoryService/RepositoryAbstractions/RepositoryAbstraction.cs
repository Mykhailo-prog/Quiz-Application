using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public class RepositoryAbstraction<T> : IRepository<T> where T : class
    {
        private protected QuizContext _context;
        private protected DbSet<T> _dbSet;
        public RepositoryAbstraction(QuizContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public virtual async Task<T> GetByID(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual async Task<UserManagerResponse> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Delete operation failed!",
                    Errors = new List<string> { $"{typeof(T).GenericTypeArguments.FirstOrDefault()} with this id not found" }
                };
            }

            _dbSet.Remove(entity);

            Save();

            return new UserManagerResponse
            {
                Success = true,
                Message = $"{typeof(T).GenericTypeArguments.FirstOrDefault()} deleted successfully!"
            };
        }
    }
}
