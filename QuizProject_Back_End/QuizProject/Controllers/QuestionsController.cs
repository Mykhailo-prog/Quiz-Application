using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using QuizProject.Services.DataTransferService;
using QuizProject.Services.RepositoryService;
using QuizProject.Models.Entity;
using QuizProject.Models;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IQuestionRepository<Question, QuestionDTO> _repository;

        public QuestionsController(RepositoryFactory factory, ILogger<QuestionsController> logger)
        {
            _repository = factory.GetRepository<IQuestionRepository<Question, QuestionDTO>>();
            _logger = logger;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<IEnumerable<Question>> GetQuestions()
        {
            return await _repository.GetAll();
        }

        // GET: api/Questions/id
        [HttpGet("id")]
        public async Task<IActionResult> GetQuestion([FromQuery]string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var question = await _repository.GetByID(int.Parse(id));

            if (question == null)
            {
                return NotFound("No question with this Id");
            }

            return Ok(question);
        }

        // PUT: api/Questions
        [HttpPut]
        public async Task<IActionResult> PutQuestion([FromQuery]string id, [FromBody]QuestionDTO questiondto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrct Id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.Update(int.Parse(id) ,questiondto);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);

        }

        // POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion([FromBody]QuestionDTO questiondto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.Create(questiondto);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        // DELETE: api/Questions
        [HttpDelete]
        public async Task<ActionResult<QuestionDTO>> DeleteQuestion([FromQuery]string id)
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
