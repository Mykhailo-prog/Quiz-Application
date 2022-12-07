using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Models.ResponseModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class UserRepositoryAbstraction<T, K> : RepositoryAbstraction<T>, IUserRepository<T, K> where T : class
    {
        protected UserRepositoryAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<T> GetByName(string name);
        public abstract Task<UserManagerResponse> Create(K item);
        public abstract Task<UserManagerResponse> Delete(string name);
        public abstract Task<UserManagerResponse> UpdateScore(int id, UserUpdateDTO item);
    }
}
