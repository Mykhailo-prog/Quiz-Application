using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuizProject.Models.DTO;
using QuizProject.Servieces;
using System.Threading.Tasks;


namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authServiece;
        private IConfiguration _configuration;

        public AuthController(IAuthService authServiece, IConfiguration configuration)
        {
            _authServiece = authServiece;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authServiece.RegisterUserAsync(model);
                if (result.Success)
                    return Ok(result);

                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authServiece.LoginUserAsync(model);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Something is invalid");
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _authServiece.ConfirmEmailAsync(userId, token);

            if (result.Success)
            {
                return Redirect(_configuration["FrontUrl"]);
            }

            return BadRequest(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            var result = await _authServiece.ForgetPasswordAsync(email);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authServiece.ResetPasswordAsync(model);

                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Smth goes Wrong!");
        }
    }
}
