using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly QuizContext _context;
        private ITestLogic _testLogic;
        public StatisticController(QuizContext context, ITestLogic testLogic)
        {
            _context = context;
            _testLogic = testLogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestStatistic>>> GetStatistic()
        {
            return await _context.Statistics.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<TestStatisticDTO>> PostStatistic(TestStatisticDTO statdto)
        {
            var stat = new TestStatistic
            {
                TestId = statdto.TestId,
            };
            _context.Statistics.Add(stat);
            await _context.SaveChangesAsync();

            return Ok(_testLogic.TestStatToDTO(stat));
        }
        [HttpDelete]
        public async Task<ActionResult<TestStatistic>> DeleteStat (int id)
        {
            var stat = await _context.Statistics.FindAsync(id);
            _context.Statistics.Remove(stat);
            await _context.SaveChangesAsync();
            return Ok(stat);
        }
    }
     
}
