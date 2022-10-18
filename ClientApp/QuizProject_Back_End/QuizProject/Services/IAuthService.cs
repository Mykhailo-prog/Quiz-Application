using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Servieces
{
    public interface IAuthService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginModel model);
        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<UserManagerResponse> ForgetPasswordAsync(string email);
        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordModel model);
        Task<UserManagerResponse> CheckRole(QuizUser qUser);
    }
    public class AuthService : IAuthService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        private QuizContext _context;
        private IEmailService _emailService;
        RoleManager<IdentityRole> _roleManager;
        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration, QuizContext context, IEmailService emailService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _emailService = emailService;

        }
        public async Task<UserManagerResponse> CheckRole(QuizUser qUser)
        {
            var user = await _userManager.FindByNameAsync(qUser.Login);
            if (user != null)
            {
                foreach (var role in await _userManager.GetRolesAsync(user))
                {
                    if (role == "Admin")
                        return new UserManagerResponse
                        {
                            Success = true,
                            Message = "Admin role confirmed!"
                        };
                }
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Not Admin"
                };
            }
            return new UserManagerResponse
            {
                Success = false,
                Message = "No user with this login"
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
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

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                new UserManagerResponse
                {
                    Message = "No user registered with this email",
                    Success = false
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["FrontUrl"]}resetpassword?email={email}&token={validToken}";

            await _emailService.SendEmailAsync(email, "Reset Password", "<h1>Here we are, to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>click here</a></p>");

            return new UserManagerResponse
            {
                Message = "Reset url created and sent successfully!",
                Success = true,
            };
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);
            var qUser = await _context.QuizUsers.FirstOrDefaultAsync(u => u.Login == model.Login);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No user registered with this login!",
                    Success = false
                };
            }
            else if (!user.EmailConfirmed)
            {
                return new UserManagerResponse
                {
                    Message = "Your email is not confirmed!",
                    Success = false
                };
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid passport",
                    Success = false
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            if (userRoles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, userRoles.FirstOrDefault()));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = "Logged in successfully!",
                Token = tokenAsString,
                User = qUser,
                Success = true,
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

        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No user registered with this email!",
                    Success = false
                };
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Password not confirmed!",
                    Success = false
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    Success = true
                };
            }
            return new UserManagerResponse
            {
                Message = "Password wasn't reset!",
                Success = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }
    }
}