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

        //TODO: For better compability I recomend to use next approach to return something from actions. Same for other controllers/actions.

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAnswers(string id)
        //{ 
        //    if (string.IsNullOrWhiteSpace(id)) return BadRequest("params Id can not be null");

        //    var item = await _context.Answers.FirstOrDefaultAsync(f => f.Id == int.Parse(id));

        //    if (item == null) return NotFound();

        //    return Ok(item);
        //}

        //DONE

        // GET: api/Answers
        [HttpGet]
        public async Task<IActionResult> GetAnswers()
        {
            return Ok(await _repository.GetAll());
            
        }

        // GET: api/Answers/5
        [HttpGet("id")]
        public async Task<ActionResult<Answer>> GetAnswer([FromQuery]string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorect Id");
            }
            //TODO: here you need to check if id has valid value. Same for other controllers/actions.

            //var answer = await _context.Answers.FirstOrDefaultAsync( f=> f.Id == id);
            //TODO: after check if answer found.
            //if (answer == null) return BadRequest();


            //DONE
            var answer = await _repository.GetByID(int.Parse(id));

            if(answer == null)
            {
                return NotFound("This answer not found");
            }

            return Ok(answer);
            
        }

        [HttpPut]
        public async Task<IActionResult> PutAnswer(string id, AnswerDTO answerdto)
        {
            //TODO: Maybe it will be good idea to add model validation in next way:
            // And use [Require] [MaxLength] etc. attributes for DTO props. see AnswerDTO.
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //Same for other controllers/actions.

            //DONE

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
            //TODO: here we will not need if we will return BadRequest() if answer == null. Same for other controllers/actions.
            //TODO: Maybe we need to add any kind of loggin? Serilog I think will be good solution. Same for other controllers/actions.
            //DONE
            //var answer = await _context.Answers.FirstOrDefaultAsync( f=> f.Id == id);
            //TODO: if (answer == null) return BadRequest()
            //Same for other controllers/actions.
            //DONE
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAnswer(List<AnswerDTO> answersdto)
        {
            //TODO: Maybe it will be good idea to add model validation in next way:
            // And use [Require] [MaxLength] etc. attributes for DTO props. see AnswerDTO.
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //Same for other controllers/actions.

            //DONE

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

        // DELETE: api/Answers/5
        [HttpDelete]
        public async Task<ActionResult<AnswerDTO>> DeleteAnswer(string id)
        {
            //TODO: same, check if id has valid value. Same for other controllers/actions.
            //TODO: can be simplified

            //DONE
            

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
