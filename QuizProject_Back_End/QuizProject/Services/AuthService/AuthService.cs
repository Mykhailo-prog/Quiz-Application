using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizProject.Models.DTO;
using QuizProject.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models.AppData;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using QuizProject.Services.EmailService;
using QuizProject.Services.RepositoryService;

namespace QuizProject.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private UserManager<IdentityUser> _userManager;
        private readonly AppConf App;
        private IEmailService _emailService;
        private readonly ILogger<AuthService> _logger;
        private readonly IUserRepository<QuizUser, UserDTO> _repository;
        public AuthService(RepositoryFactory factory, UserManager<IdentityUser> userManager, IEmailService emailService, IOptions<AppConf> options, ILogger<AuthService> logger)
        {
            _repository = factory.GetRepository<IUserRepository<QuizUser, UserDTO>>();
            _userManager = userManager;
            _emailService = emailService;
            App = options.Value;
            _logger = logger;

        }
        public async Task<UserManagerResponse> CheckRole(string login)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(login);

                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Check role operation failed",
                        Errors = new List<string> { "No user with this login" }
                    };
                }

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
                    Message = "Not Admin",
                    Errors = new List<string> { "Not Admin" }
                };
            }
            catch(Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Check role operation failed",
                    Errors = new List<string> { e.Message }
                };
            }

        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Confirm email operation failed",
                        Errors = new List<string> { "No user with this login" }
                    };
                }

                var decodedToken = WebEncoders.Base64UrlDecode(token);
                var normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _userManager.ConfirmEmailAsync(user, normalToken);

                if (!result.Succeeded)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Email confirmation failed!",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Email confirmed successfully!",
                };

            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Confirm email operation failed",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Forget password operation failed",
                        Errors = new List<string> { "No user with this login" }
                    };
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);

                string url = $"{App.FrontUrl}resetpassword?email={email}&token={validToken}";

                var result = await _emailService.SendEmailAsync(email, "Reset Password", "<h1>Here we are, to reset your password</h1>" +
                    $"<p>To reset your password <a href='{url}'>click here</a></p>");

                if (!result.Success)
                {
                    return result;
                }
                return new UserManagerResponse
                {
                    Message = "Reset url created and sent successfully!",
                    Success = true,
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Forget password operation failed",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Login);
                var qUser = await _repository.GetByName(model.Login);

                if (user == null || qUser == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Login operation failed",
                        Errors = new List<string> { "No user with this login" }
                    };
                }
                else if (!user.EmailConfirmed)
                {
                    return new UserManagerResponse
                    {
                        Message = "Login operation failed",
                        Success = false,
                        Errors = new List<string> { "Your email is not confirmed!" }

                    };
                }
                var result = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!result)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Login operation failed",
                        Errors = new List<string> { "Incorrect password" }
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


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(App.Jwt.Key));

                var token = new JwtSecurityToken(
                    issuer: App.Jwt.Issuer,
                    audience: App.Jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

                return new UserManagerResponse<QuizUser>
                {
                    Message = "Logged in successfully!",
                    Token = tokenAsString,
                    Object = qUser,
                    Success = true,
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Login operation failed",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel model)
        {
            try
            {
                if (model.Password != model.ConfirmPassword)
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Register operation failed",
                        Errors = new List<string> { "Password not confirmed" }
                    };

                var identityUser = new IdentityUser
                {
                    Email = model.EmailAddress,
                    UserName = model.Login == "" ? model.EmailAddress : model.Login,
                };

                var result = await _userManager.CreateAsync(identityUser, model.Password);

                if (!result.Succeeded)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Register operation failed",
                        Errors = result.Errors.Select(e => e.Description).ToList(),
                    };
                }


                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{App.AppUrl}/api/auth/confirmemail?userId={identityUser.Id}&token={validEmailToken}";

                var res = await _emailService.SendEmailAsync(identityUser.Email, "Confirm your Email", "<h1>Thanks to using Quiz App!</h1>" + $"<p>To confirm your email <a href='{url}'>click here</a></p>");

                if (!res.Success)
                {
                    return res;
                }

                var createUserResult = await _repository.Create(new UserDTO { Login = model.Login });

                if (!createUserResult.Success)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Register operation failed",
                        Errors = createUserResult.Errors
                    };
                }

                var user = await _repository.GetByName(model.Login);

                return new UserManagerResponse<QuizUser>
                {
                    Success = true,
                    Message = "User has beed registered successfully!",
                    Object = user
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Register operation failed",
                    Errors = new List<string> { e.Message }
                };
            }

        }

        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Reset password operation failed",
                        Errors = new List<string> { "No user with this email" }
                    };
                }

                if (model.Password != model.ConfirmPassword)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Reset password operation failed",
                        Errors = new List<string> { "Password not confirmed" }
                    };
                }

                var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
                var normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _userManager.ResetPasswordAsync(user, normalToken, model.Password);

                if (result.Succeeded)
                {
                    return new UserManagerResponse
                    {
                        Message = "Reset password operation failed",
                        Success = false,
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Reset password operation failed",
                    Errors = new List<string> { e.Message }
                };
            }
        }
    }
}
