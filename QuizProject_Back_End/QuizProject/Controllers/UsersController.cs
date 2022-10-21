using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using QuizProject.Services;
using Microsoft.AspNetCore.Identity;
using QuizProject.Servieces;
using Serilog;
using Microsoft.Extensions.Logging;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QuizContext _context;
        private ITestLogic _testLogic;
        private IAuthService _authService;
        private readonly IDataTransferServise _dto;
        private UserManager<IdentityUser> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(QuizContext context, ITestLogic testLogic, UserManager<IdentityUser> userManager, ILogger<UsersController> logger, IAuthService authService, IDataTransferServise dto)
        {
            _authService = authService;
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _testLogic = testLogic;
            _dto = dto;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizUser>>> GetUsers()
        {
            await _context.CreatedTests.LoadAsync();
            return await _context.QuizUsers.Include(u => u.UserTestCount).ToListAsync();
        }

        // GET: api/Users/id
        [HttpGet("id")]
        public async Task<ActionResult<QuizUser>> GetUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var user = await _context.QuizUsers.FindAsync(int.Parse(id));

            if (user == null)
            {
                return NotFound("No user with this Id");
            }

            return Ok(user);
        }
        // POST: api/users/checkrole
        [HttpPost("checkrole")]
        public async Task<bool> CheckRole(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }
            var result = await _authService.CheckRole(login);

            if (result.Success)
            {
                return true;
            }

            return false;
        }
        [HttpPost("resetscore")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetUserScore(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Incorrect login");
            }
            var user = await _context.QuizUsers.FirstOrDefaultAsync(u =>u.Login == name);
            if(user == null)
            {
                return NotFound("No user with this login");
            }

            user.Score = 0;

            await _context.SaveChangesAsync();
            return Ok();
            
        }

        [HttpPost("changepass")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminChangePassword(string name, string password)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Incorrect name or password ");
            }

            var user = await _userManager.FindByNameAsync(name);
            if(user == null)
            {
                return NotFound("No user with this login!");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (!result.Succeeded)
            {
                return BadRequest("Change password Failed!");
            }

            return Ok();
        }

        [HttpPost("adminconfirm")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminConfirmEmail(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Incorrect login");
            }

            var user = await _userManager.FindByNameAsync(name);

            if(user == null)
            {
                return NotFound("No user with this login!");
            }

            if (user.EmailConfirmed)
            {
                return BadRequest("Email already confirmed");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return BadRequest("Email confirmation has beed failed");
            }

            return Ok();
        }

        // PUT: api/Users/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutUser(string id, UserUpdateDTO userUpdto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _context.QuizUsers.FindAsync(int.Parse(id));

                if(user == null)
                {
                    return NotFound("No user with this Id");
                }

                var test = await _context.Tests.Include(t => t.Questions).FirstOrDefaultAsync(t => t.TestId == userUpdto.Test);

                if(test == null)
                {
                    return BadRequest("No test with this Id");
                }

                var result = _testLogic.GetScore(user, userUpdto, test);

                if (!_context.UserStatistic.Where(e => e.QuizUserId == user.Id).Any(u => u.TestId == userUpdto.Test))
                {
                    await _testLogic.CreateStatisticAsync(user, test, result);
                }
                else
                {
                    await _testLogic.UpdateStatistic(user, test, result);
                }
                
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (DbUpdateConcurrencyException err)
            {
                _logger.LogError("Db update error!\n{0}", err.Message);
                return BadRequest(err.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Database wasn't updated. Error on updating user with id:{0} \nReason: {1}", id, e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<QuizUser>> PostUser(UserDTO userdto)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new QuizUser
            {
                Login = userdto.Login,
            };

            _context.QuizUsers.Add(user);
            await _context.SaveChangesAsync();

            return Ok(_dto.UserToDTO(user));
        }

        // DELETE: api/Users
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Incorrect login");
            }
            var user = await _userManager.FindByNameAsync(name);

            if(user == null)
            {
                return NotFound("User not found");
            }

            await _userManager.DeleteAsync(user);

            var qUser = await _context.QuizUsers.FirstOrDefaultAsync(u => u.Login == name);

            if (qUser == null)
            {
                return NotFound("User not found");
            }

            _context.QuizUsers.Remove(qUser);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
