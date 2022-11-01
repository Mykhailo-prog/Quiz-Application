using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Models.Entity;
using QuizProject.Models.ResponseModels;
using QuizProject.Services.RepositoryService.Interfaces;
using QuizProject.Services.RepositoryService.RepositoryAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Repositories
{
    public class UserRepository : UserRepositoryAbstraction<QuizUser, UserDTO>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStatisticRepository<UserStatistic, TestLogicContainer<FinishTestResponse>> _repository;

        public UserRepository(QuizContext context, UserStatisticRepository repo, UserManager<IdentityUser> userManager) : base(context)
        {
            _userManager = userManager;
            _repository = repo;
        }

        public override async Task<IEnumerable<QuizUser>> GetAll()
        {
            await _context.UserCreatedTests.LoadAsync();
            return await _dbSet.Include(u => u.UserTestCount).ToListAsync();
        }

        public override async Task<QuizUser> GetByID(int id)
        {
            await _context.UserCreatedTests.LoadAsync();
            await _context.UserStatistic.LoadAsync();
            var user = await _dbSet.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public override async Task<QuizUser> GetByName(string name)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Login == name);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public override async Task<UserManagerResponse> Create(UserDTO item)
        {
            try
            {
                var user = new QuizUser
                {
                    Login = item.Login,
                    Score = 0
                };
                await _dbSet.AddAsync(user);

                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "User has been created successfully!"
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Creating user proccess failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public override async Task<UserManagerResponse> Delete(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Updating score operation failed!",
                    Errors = new List<string> { "Identity user not Found" }
                };
            }

            await _userManager.DeleteAsync(user);

            var qUser = await _dbSet.FirstOrDefaultAsync(u => u.Login == name);

            if (qUser == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Updating score operation failed!",
                    Errors = new List<string> { "Quiz user not Found" }
                };
            }

            _dbSet.Remove(qUser);
            Save();

            return new UserManagerResponse
            {
                Success = true,
                Message = "User has been created successfully!"
            };
        }

        public override async Task<UserManagerResponse> UpdateScore(int id, UserUpdateDTO item)
        {
            try
            {
                var user = await _dbSet.FindAsync(id);

                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Updating score operation failed!",
                        Errors = new List<string> { "User not Found" }
                    };
                }

                var test = await _context.Tests.Include(t => t.Questions).FirstOrDefaultAsync(t => t.TestId == item.Test);

                if (test == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Updating score operation failed!",
                        Errors = new List<string> { "Test not Found" }
                    };
                }

                var Ucontainer = new TestLogicContainer<UserUpdateDTO>
                {
                    User = user,
                    Test = test,
                    Result = item
                };

                var result = GetScore(Ucontainer);

                if (!result.Success)
                {
                    return result;
                }

                var Tcontainer = new TestLogicContainer<FinishTestResponse>
                {
                    User = user,
                    Test = test,
                    Result = result.Object
                };

                var res = await ChangeUserStatistic(Tcontainer, item.Test);

                if (!res.Success)
                {
                    return res;
                }

                Save();

                return result;
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Updating score operation failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        private async Task<UserManagerResponse> ChangeUserStatistic(TestLogicContainer<FinishTestResponse> container, int id)
        {
            if (_repository.IsExists(container, id))
            {
                return await _repository.Update(container);
            }
            else
            {
                return await _repository.Create(container);
            }
        }

        private UserManagerResponse<FinishTestResponse> GetScore(TestLogicContainer<UserUpdateDTO> container)
        {
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
