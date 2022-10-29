using QuizProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    /*public interface IRepositoryttt<T, K> : IDisposable, IManyExtention<T,K> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetByID(int id);
        public Task<UserManagerResponse> Create(K item);
        public Task<UserManagerResponse> Delete(int id);
        public Task<UserManagerResponse> Update(int id, K item);
        void Save();
    }*/
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetByID(int id);
        public Task<UserManagerResponse> Delete(int id);
        void Save();
    }
}
