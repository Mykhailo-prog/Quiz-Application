﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.AdministratorService
{
    public class AdministratorService : IAdministratorService
    {
        private readonly QuizContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministratorService(QuizContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserManagerResponse> ResetScore(string name)
        {
            var user = await _context.QuizUsers.FirstOrDefaultAsync(u => u.Login == name);
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

            await _context.SaveChangesAsync();

            return new UserManagerResponse
            {
                Success = true,
                Message = "User's score has been reset successfully!"
            };

        }

        public async Task<UserManagerResponse> ChangePassword(string name, string password)
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

        public async Task<UserManagerResponse> ConfirmEmail(string name)
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
