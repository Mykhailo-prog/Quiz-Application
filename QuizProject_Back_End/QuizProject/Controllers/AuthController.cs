using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuizProject.Models.AppData;
using QuizProject.Models.DTO;
using QuizProject.Services.AuthService;
using System.Threading.Tasks;


namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authServiece;
        private readonly AppConf _appConf;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IAuthService authServiece, IOptions<AppConf> options)
        {
            _logger = logger;
            _authServiece = authServiece;
            _appConf = options.Value;
        }

        // POST: api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authServiece.RegisterUserAsync(model);

            if (!result.Success)
            {
                foreach (var e in result.Errors)
                {
                    _logger.LogError("Error : {0}", e);
                }

                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST: api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authServiece.LoginUserAsync(model);

            if (!result.Success)
            {
                foreach (var e in result.Errors)
                {
                    _logger.LogError("Error : {0}", e);
                }

                return BadRequest(result);
            }

            return Ok(result);
        }

        // GET: api/auth/confirmemail
        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _authServiece.ConfirmEmailAsync(userId, token);

            if (!result.Success)
            {
                foreach (var e in result.Errors)
                {
                    _logger.LogError("Error : {0}", e);
                }

                return BadRequest(result);
            }

            return Redirect(_appConf.FrontUrl);
            
        }

        // POST: api/auth/forgetpassword
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery]string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            var result = await _authServiece.ForgetPasswordAsync(email);

            if (!result.Success)
            {
                foreach (var e in result.Errors)
                {
                    _logger.LogError("Error : {0}", e);
                }

                return BadRequest(result);
            }

            return Ok(result);
            
        }

        // POST: api/auth/resetpassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authServiece.ResetPasswordAsync(model);

            if (result.Success)
            {
                foreach (var e in result.Errors)
                {
                    _logger.LogError("Error : {0}", e);
                }

                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
