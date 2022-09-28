using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Servieces
{
    public interface IAuthService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterModel model);
        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
    }
    public class AuthService : IAuthService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        private QuizContext _context;
        private IEmailService _emailService;
        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration, QuizContext context, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _emailService = emailService;

        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "User not Found"
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Email confirmed successfully!",
                };
            }

            return new UserManagerResponse
            {
                Success = false,
                Message = "Email confirmation failed!",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel model)
        {
            if (model == null)
                throw new NullReferenceException("Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Password not confirmed",
                    Success = false,
                };
            var identityUser = new IdentityUser
            {
                Email = model.EmailAddress,
                UserName = model.Login == "" ? model.EmailAddress : model.Login,
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                //TODO: Generate token

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userId={identityUser.Id}&token={validEmailToken}";

                await _emailService.SendEmailAsync(identityUser.Email, "Confirm your Email", "<h1>Thanks to using Quiz App!</h1>" + $"<p>To confirm your email <a href='{url}'>click here</a></p>");

                var user = new QuizUser
                {
                    Login = model.Login,
                    Score = 0,
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "User has beed registered successfully!",
                    User = user
                };
            }
            return new UserManagerResponse
            {
                Success = false,
                Message = "Registration fails",
                Errors = result.Errors.Select(e => e.Description),
            };
        }
    }
}