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
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _authServiece.ConfirmEmailAsync(userId, token);

            if (result.Success)
            {
                Redirect(_configuration["FrontUrl"]);
            }

            return BadRequest(result);
        }
    }
}
