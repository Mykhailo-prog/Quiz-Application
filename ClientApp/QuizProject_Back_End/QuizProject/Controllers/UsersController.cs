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

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QuizContext _context;
        private ITestLogic _testLogic;
        private IAuthService _authService;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UsersController(QuizContext context, ITestLogic testLogic, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _authService = authService;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _testLogic = testLogic;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizUser>>> GetUsers()
        {
            _context.CreatedTests.Load();
            return await _context.QuizUsers.Include(u => u.UserTestCount).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizUser>> GetUser(int id)
        {
            var user = await _context.QuizUsers.FindAsync(id);

            if (!_testLogic.ElemExists<QuizUser>(id))
            {
                return NotFound();
            }

            return user;
        }
        // POST: api/users/checkrole
        [HttpPost("checkrole")]
        public async Task<bool> CheckRole(QuizUser qUser)
        {
            var result = await _authService.CheckRole(qUser);

            if (result.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost("resetscore")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetUserScore(string name)
        {
            var user = await _context.QuizUsers.FirstOrDefaultAsync(u =>u.Login == name);
            if(user != null)
            {
                user.Score = 0;
                await _context.SaveChangesAsync();

                return StatusCode(200);
            }

            return NotFound("No user with this login");
        }

        [HttpPost("changepass")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminChangePassword(string name, string password)
        {
            var user = await _userManager.FindByNameAsync(name);
            if(user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, password);

                if (result.Succeeded)
                {
                    return StatusCode(200);
                }

                return BadRequest("Change password Failed!");
            }

            return NotFound("No user with this login!");
        }

        [HttpPost("adminconfirm")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminConfirmEmail(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if(user != null)
            {
                if (user.EmailConfirmed)
                {
                    return BadRequest("Email already confirmed");
                }
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return StatusCode(200);
                }

                return BadRequest("Email confirmation has beed failed");
            }

            return NotFound("No user with this login!");
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, UserUpdateDTO userUpdto)
        {

            try
            {
                var user = await _context.QuizUsers.FindAsync(id);
                var test = await _context.Tests.Include(t => t.Questions).FirstOrDefaultAsync(t => t.TestId == userUpdto.Test);
                if(user == null)
                {
                    return NotFound("No user with this ID");
                }else if(test == null)
                {
                    return NotFound("Test not found!");
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
            catch (DbUpdateConcurrencyException)
            {
                if (!_testLogic.ElemExists<QuizUser>(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                return BadRequest("User wasn't updated!");
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<QuizUser>> PostUser(UserDTO userdto)
        {
            var user = new QuizUser
            {
                Login = userdto.Login,
            };
            _context.QuizUsers.Add(user);
            await _context.SaveChangesAsync();

            return Ok(_testLogic.UserToDTO(user));
        }

        // DELETE: api/Users
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
                
                var qUser = await _context.QuizUsers.FirstOrDefaultAsync(u => u.Login == name);

                if(qUser != null)
                {
                    _context.QuizUsers.Remove(qUser);
                    await _context.SaveChangesAsync();

                    return StatusCode(200);
                }

                return NotFound("User not found");
            }

            return NotFound("User not found");
        }
    }
}
