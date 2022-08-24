using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Functions;
using Microsoft.AspNetCore.Cors;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QuizContext _context;

        public UsersController(QuizContext context)
        {
            _context = context;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _context.CreatedTests.Load();
            return await _context.Users.Include(u => u.UserTestCount).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (!Methods.ElemExists<User>(id, _context))
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpdateDTO userUpdto)
        {

            try
            {
                if (!Methods.ElemExists<User>(id, _context))
                {
                    return NotFound();
                }
                var editUser = await _context.Users.FindAsync(id);

                editUser.Login = userUpdto.Login;
                editUser.Password = userUpdto.Password;
                editUser.Score = Methods.GetScore(userUpdto.userAnswers, userUpdto.Test, userUpdto.Score, _context).Result;
                if (_context.Tests.Any(t => t.TestId == userUpdto.Test) && !_context.UserTests.Any(u => u.TestTried == userUpdto.Test))
                {
                    var testtried = new UserTestCount
                    {
                        TestTried = userUpdto.Test,
                        UserId = id,
                    };
                    _context.UserTests.Add(testtried);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Methods.ElemExists<User>(id, _context))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userdto)
        {
            var user = new User
            {
                Login = userdto.Login,
                Password = userdto.Password,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(Methods.UserToDTO(user));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (!Methods.ElemExists<User>(id, _context))
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Methods.UserToDTO(user);
        }
    }
}
