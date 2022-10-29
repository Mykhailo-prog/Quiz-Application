using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
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
    /*abstract public class Repository<T, K> : IRepository<T, K> where T : class
    {
        private protected QuizContext _context;
        private protected readonly DbSet<T> _dbSet;
        private bool disposed = false;
        public Repository(QuizContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

        }
        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

         public void Save()
        {
            _context.SaveChanges();
        }

        public async virtual Task<T> GetByID(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if(entity == null)
            {
                return null;
            }

            return entity;
        }

        abstract public Task<UserManagerResponse> Create(K item);
        abstract public Task<UserManagerResponse> Update(int id, K item);
        public async virtual Task<UserManagerResponse> CreateMany(List<K> items)
        {
            return new UserManagerResponse
            {
                Success = false,
                Message = "This method hadn't beed overrided in specifide repository!"
            };
        }

        public async Task<UserManagerResponse> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if(entity == null)
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

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //Warnning!
        //This method exists only for AnswerRepository, to override it
        //By the way this is offence of Interface Segrigation princeple 
        
    }*/
}
