using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetByID(int id);
        public Task<UserManagerResponse> Delete(int id);
        void Save();
    }
}
