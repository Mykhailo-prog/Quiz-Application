using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.TestLogic;
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

        public override async Task<UserManagerResponse> Delete(int id)
        {
            return new UserManagerResponse
            {
                Success = false,
                Message = "Delete operation failed",
                Errors = new List<string> { "User can't be deleted by id, only by name!" }
            };
        }
        public abstract Task<T> GetByName(string name);
        public abstract Task<UserManagerResponse> Create(K item);
        public abstract Task<UserManagerResponse> Delete(string name);
        public abstract Task<UserManagerResponse> UpdateScore(int id, UserUpdateDTO item);

        public UserManagerResponse<FinishTestResponse> GetScore(TestLogicContainer<UserUpdateDTO> container)
        {
            //TODO: Are you sure you want to get every time all Questions??? What will happened if we will have 1 billion records in Question table?
            //Please remember about IQuerible and IEnumerable
            //Here i get questions of current test

            try
            {
                var questions = container.Test.Questions.ToList();
                double proventResult = 0;

                for (int i = 0; i < container.Result.userAnswers.Count; i++)
                {
                    if (container.Result.userAnswers[i] == questions[i].CorrectAnswer)
                    {
                        proventResult++;
                        container.User.Score += 10;
                    }
                }

                var result = new FinishTestResponse
                {
                    UserName = container.User.Login,
                    Time = container.Result.Time,
                    Result = Convert.ToInt32(Math.Round(proventResult * 100 / container.Result.userAnswers.Count)),
                };
                return new UserManagerResponse<FinishTestResponse>
                {
                    Success = true,
                    Message = "User score counted successfully!",
                    Object = result
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse<FinishTestResponse>
                {
                    Success = false,
                    Message = "User score count failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }
    }
}
