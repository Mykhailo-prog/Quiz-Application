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
using QuizProject.Services;
using Serilog;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswersController : ControllerBase
    {
        private readonly IDataTransferServise _dto;
        private readonly QuizContext _context;
        private readonly ILogger<AnswersController> _logger;

        public AnswersController(QuizContext context, IDataTransferServise dataTransferServise, ILogger<AnswersController> logger)
        {
            _logger = logger;
            _context = context;
            _dto = dataTransferServise;
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
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            return await _context.Answers.ToListAsync();
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
            var answer = await _context.Answers.FindAsync(int.Parse(id));

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

            try
            {
                var answer = await _context.Answers.FindAsync(int.Parse(id));

                if (answer == null)
                {
                    return NotFound("No answer with this Id");
                }

                //var answer = await _context.Answers.FirstOrDefaultAsync( f=> f.Id == id);
                //TODO: if (answer == null) return BadRequest()
                //Same for other controllers/actions.
                //DONE

                answer.Ans = answerdto.Answer;
                answer.QuestionId = answerdto.QuestionId;

                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException err)
            {
                _logger.LogError("Db update error!\n{0}", err.Message);
                return BadRequest(err.Message);
            }
            catch (Exception e )
            {
                //TODO: here we will not need if we will return BadRequest() if answer == null. Same for other controllers/actions.
                //TODO: Maybe we need to add any kind of loggin? Serilog I think will be good solution. Same for other controllers/actions.
                //DONE
                _logger.LogError("Database wasn't updated. Error on updating Answer with id:{0} \nReason: {1}", id, e.Message);
                return BadRequest(e.Message);   
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<List<AnswerDTO>>> PostAnswer(List<AnswerDTO> answersdto)
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

            foreach (AnswerDTO ans in answersdto)
            {
                var answer = new Answer
                {
                    Ans = ans.Answer,
                    QuestionId = ans.QuestionId
                };
                _context.Answers.Add(answer);
            }
            
            await _context.SaveChangesAsync();

            return Ok(answersdto);
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

            var answer = await _context.Answers.FindAsync(int.Parse(id));
            if (answer == null)
            {
                return NotFound("No answer with this Id");
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return Ok(_dto.AnswerToDTO(answer));
        }
    }
}
