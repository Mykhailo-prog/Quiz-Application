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

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QuizContext _context;
        private ITestLogic _testLogic;

        public UsersController(QuizContext context, ITestLogic testLogic)
        {
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> DeleteUser(int id)
        {
            var user = await _context.QuizUsers.FindAsync(id);
            if (!_testLogic.ElemExists<QuizUser>(id))
            {
                return NotFound();
            }

            _context.QuizUsers.Remove(user);
            await _context.SaveChangesAsync();

            return _testLogic.UserToDTO(user);
        }
    }
}
