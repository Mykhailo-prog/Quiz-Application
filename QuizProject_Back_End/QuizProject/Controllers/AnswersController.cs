using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuizProject.Models;
using QuizProject.Models.AppData;
using QuizProject.Models.DTO;
using QuizProject.Models.Entity;
using QuizProject.Services.DataTransferService;
using QuizProject.Services.RepositoryService;
using Serilog;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswersController : ControllerBase
    {
        private readonly ILogger<AnswersController> _logger;
        private readonly IAnswerRepository<Answer, AnswerDTO> _repository;

        public AnswersController(ILogger<AnswersController> logger, RepositoryFactory factory)
        {
            _repository = factory.GetRepository<IAnswerRepository<Answer, AnswerDTO>>();
            _logger = logger;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<IEnumerable<Answer>> GetAnswers()
        {
            return await _repository.GetAll();
            
        }

        // GET: api/Answers/id
        [HttpGet("id")]
        public async Task<IActionResult> GetAnswer([FromQuery]string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorect Id");
            }

            var answer = await _repository.GetByID(int.Parse(id));

            if(answer == null)
            {
                return NotFound("This answer not found");
            }

            return Ok(answer);
            
        }

        // PUT: api/Answers
        [HttpPut]
        public async Task<IActionResult> PutAnswer([FromForm]string id, [FromBody]AnswerDTO answerdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect id!");
            }

            var result = await _repository.Update(int.Parse(id), answerdto);

            if (!result.Success)
            {
                _logger.LogError(result.Message);

                foreach(var e in result.Errors)
                {
                    _logger.LogError("Error : {0}", e);
                }

                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST: api/Answers
        [HttpPost]
        public async Task<IActionResult> PostAnswer([FromBody]List<AnswerDTO> answersdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.Create(answersdto);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        // DELETE: api/Answers
        [HttpDelete]
        public async Task<ActionResult<AnswerDTO>> DeleteAnswer([FromQuery] string id)
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
