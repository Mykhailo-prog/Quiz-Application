using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    //TODO: Is it not athorized controller?
    //DONE
    public class StatisticController : ControllerBase
    {
        private readonly QuizContext _context;
        private readonly IDataTransferServise _dto;
        public StatisticController(QuizContext context, IDataTransferServise dto)
        {
            _context = context;
            _dto = dto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestStatistic>>> GetStatistic()
        {
            return await _context.Statistics.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<TestStatisticDTO>> PostStatistic(TestStatisticDTO statdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stat = new TestStatistic
            {
                TestId = statdto.TestId,
            };

            _context.Statistics.Add(stat);
            await _context.SaveChangesAsync();

            return Ok(_dto.TestStatToDTO(stat));
        }
        [HttpDelete]
        public async Task<ActionResult<TestStatistic>> DeleteStat (string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var stat = await _context.Statistics.FindAsync(int.Parse(id));

            if (stat == null)
            {
                return NotFound("No statistic with this Id");
            }

            _context.Statistics.Remove(stat);
            await _context.SaveChangesAsync();

            return Ok(stat);
        }
    }
     
}
