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
            try
            {
                var answer = await _context.Answers.FindAsync(id);

                answer.Ans = answerdto.Answer;
                answer.QuestionId = answerdto.QuestionId;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_testLogic.ElemExists<Answer>(id))
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

        [HttpPost]
        public async Task<ActionResult<List<AnswerDTO>>> PostAnswer(List<AnswerDTO> answersdto)
        {
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
