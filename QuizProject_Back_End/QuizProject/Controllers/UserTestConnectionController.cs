using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Functions;
using QuizProject.Models;
using QuizProject.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestConnectionController : ControllerBase
    {
        private readonly QuizContext _context;

        public UserTestConnectionController(QuizContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCreatedTest>>> GetTestConnection()
        {
            return await _context.CreatedTests.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<UserCreatedTest>> PostTestConnection(CreatedTestDTO ctdto)
        {
            var uct = new UserCreatedTest
            {
                UserId = ctdto.UserId,
                TestId = ctdto.TestId,
            };
            _context.CreatedTests.Add(uct);
            await _context.SaveChangesAsync();
            return Ok(Methods.CreatedTestToDTO(uct));
        }
        [HttpPut]
        public async Task<IActionResult> PutTestConnection(int id, CreatedTestDTO ctdto)
        {
            try
            {
                var uct = await _context.CreatedTests.FindAsync(id);

                uct.TestId = ctdto.TestId;
                uct.UserId = ctdto.UserId;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(ctdto.TestId == 0 && ctdto.UserId == 0)
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
    }
}
