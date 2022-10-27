using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.RepositoryService.Interfaces;
using QuizProject.Services.RepositoryService.RepositoryAbstractions;
using QuizProject.Services.TestLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Repositories
{
    public class UserRepository : UserRepositoryAbstraction<QuizUser, UserDTO>
    {
        private readonly ITestLogic _testLogic;
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(QuizContext context, ITestLogic logic, UserManager<IdentityUser> userManager) : base(context)
        {
            _userManager = userManager;
            _testLogic = logic;
        }
        public override async Task<IEnumerable<QuizUser>> GetAll()
        {
            await _context.CreatedTests.LoadAsync();
            return await _dbSet.Include(u => u.UserTestCount).ToListAsync();
        }
        public override async Task<UserManagerResponse> Create(UserDTO item)
        {
            try
            {
                var user = new QuizUser
                {
                    Login = item.Login,
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

                var result = _testLogic.GetScore(user, item, test);

                if (!result.Success)
                {
                    return result;
                }

                var res = await _testLogic.ChangeUserStatistic(user, test, result.Object, item.Test);

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

        public override async Task<UserManagerResponse> ResetScore(string name)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Login == name);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Resting score operation failed!",
                    Errors = new List<string> { "User not Found" }
                };
            }

            user.Score = 0;

            Save();

            return new UserManagerResponse
            {
                Success = true,
                Message = "User's score has been reset successfully!"
            };

        }

        public override async Task<UserManagerResponse> ChangePassword(string name, string password)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Changing password operation failed!",
                    Errors = new List<string> { "User not Found" }
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Changing password operation failed!",
                    Errors = new List<string> { "Changing password operation failed" }
                };
            }

            return new UserManagerResponse
            {
                Success = true,
                Message = "Password has been changed successfully!"
            };
        }

        public override async Task<UserManagerResponse> ConfirmEmail(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Confirm email operation failed!",
                    Errors = new List<string> { "User not Found" }
                };
            }

            if (user.EmailConfirmed)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Confirm email operation failed!",
                    Errors = new List<string> { "Email has already confirmed" }
                };
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Confirm email operation failed!",
                    Errors = new List<string> { "Confirm email operation failed!" }
                };
            }

            return new UserManagerResponse
            {
                Success = true,
                Message = "Email has been confirmed successfully!"
            };
        }
    }
}
