using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuizProject.Models.AppData;
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
        private readonly App App;

        public AuthController(IAuthService authServiece, IOptions<App> options)
        {
            _authServiece = authServiece;
            App = options.Value;
        }

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
                return BadRequest(result);
            }

            return Ok(result);
            
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authServiece.LoginUserAsync(model);

            //TODO: I think it looks more clear.
            //if(!result.Success) return BadRequest(result);

            //return Ok(result);
            //DONE

            if (!result.Success)
            {
                return BadRequest(result);
                
            }

            //TODO: Something what? :)))))
            //DONE
            return Ok(result);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _authServiece.ConfirmEmailAsync(userId, token);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Redirect(App.FrontUrl);
            
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            var result = await _authServiece.ForgetPasswordAsync(email);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
            
        }//TODO: You need to separate every method with space, please check other places.
        //DONE

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
                return BadRequest(result);
                
            }

            return Ok(result);

            //TODO: Something what? :)))))
            //DONE
        }
    }
}
