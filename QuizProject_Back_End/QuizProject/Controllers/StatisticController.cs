using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Models.Entity;
using QuizProject.Services.DataTransferService;
using QuizProject.Services.RepositoryService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticController : ControllerBase
    {
        private readonly ILogger<StatisticController> _logger;
        private readonly IStatisticRepositiry<TestStatistic, TestStatisticDTO> _repository;
        public StatisticController(ILogger<StatisticController> logger, RepositoryFactory factory)
        {
            _logger = logger;
            _repository = factory.GetRepository<IStatisticRepositiry<TestStatistic, TestStatisticDTO>>();
        }

        // GET: api/Statistic
        [HttpGet]
        public async Task<IEnumerable<TestStatistic>> GetStatistic()
        {
            return await _repository.GetAll();
        }

        // POST: api/Statistic
        [HttpPost]
        public async Task<ActionResult<TestStatisticDTO>> PostStatistic([FromBody]TestStatisticDTO statdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.Create(statdto);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        // DELETE: api/Statistic
        [HttpDelete]
        public async Task<ActionResult<TestStatistic>> DeleteStat ([FromQuery]string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var result = await _repository.Delete(int.Parse(id));

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
     
}
