using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswersController : ControllerBase
    {
        private readonly ITestLogic _testLogic;
        private readonly QuizContext _context;

        public AnswersController(QuizContext context, ITestLogic testLogic)
        {
            _context = context;
            _testLogic = testLogic;
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

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            return await _context.Answers.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(int id)
        {

            //TODO: here you need to check if id has valid value. Same for other controllers/actions.

            //var answer = await _context.Answers.FirstOrDefaultAsync( f=> f.Id == id);
            //TODO: after check if answer found.
            //if (answer == null) return BadRequest();

            var answer = await _context.Answers.FindAsync(id);

            if (!_testLogic.ElemExists<Answer>(id))
            {
                return NotFound();
            }

            return answer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(int id, AnswerDTO answerdto)
        {
            //TODO: Maybe it will be good idea to add model validation in next way:
            // And use [Require] [MaxLength] etc. attributes for DTO props. see AnswerDTO.
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //Same for other controllers/actions.

            try
            {
                var answer = await _context.Answers.FindAsync(id);

                //var answer = await _context.Answers.FirstOrDefaultAsync( f=> f.Id == id);
                //TODO: if (answer == null) return BadRequest()
                //Same for other controllers/actions.

                answer.Ans = answerdto.Answer;
                answer.QuestionId = answerdto.QuestionId;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: here we will not need if we will return BadRequest() if answer == null. Same for other controllers/actions.
                if (!_testLogic.ElemExists<Answer>(id))
                {
                    return NotFound();
                }
                else
                {
                    //TODO: Maybe we need to add any kind of loggin? Serilog I think will be good solution. Same for other controllers/actions.
                    throw;
                }
            }

            return NoContent();
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnswerDTO>> DeleteAnswer(int id)
        {
            //TODO: same, check if id has valid value. Same for other controllers/actions.
            //TODO: can be simplified
            
            var answer = await _context.Answers.FindAsync(id);
            if (!_testLogic.ElemExists<Answer>(id))
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return _testLogic.AnswerToDTO(answer);
        }
    }
}
