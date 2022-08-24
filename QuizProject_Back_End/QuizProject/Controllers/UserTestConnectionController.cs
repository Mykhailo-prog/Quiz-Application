using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizProject.Functions;
using QuizProject.Models;
using QuizProject.Models.DTO;
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
    }
}
