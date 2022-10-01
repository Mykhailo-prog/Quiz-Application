using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Functions;
using QuizProject.Models;
using QuizProject.Models.DTO;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswersController : ControllerBase
    {
        private readonly TestLogic _testLogic;
        private readonly QuizContext _context;

        public AnswersController(QuizContext context)
        {
            _context = context;
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

        // PUT: api/Answers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(int id, AnswerDTO answerdto)
        {
            try
            {
                if (!_testLogic.ElemExists<Answer>(id))
                {
                    return NotFound();
                }
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

        // POST: api/Answers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<List<AnswerDTO>>> PostAnswer(List<AnswerDTO> answersdto)
        {
            //var quest = await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(e => e.Id == answersdto[0].QuestionId);
            foreach (AnswerDTO ans in answersdto)
            {
                var answer = new Answer
                {
                    Ans = ans.Answer,
                    QuestionId = ans.QuestionId
                };
                _context.Answers.Add(answer);
            }
            //quest.Answers.Add(answer);
            
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

            return ModelsToDto.AnswerToDTO(answer);
        }
    }
}
