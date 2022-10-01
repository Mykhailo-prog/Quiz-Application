using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Functions;
using Microsoft.AspNetCore.Authorization;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly TestLogic _testLogic;
        private readonly QuizContext _context;

        public QuestionsController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return await _context.Questions.Include(q => q.Answers).ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync( q => q.Id == id);

            if (!_testLogic.ElemExists<Question>(id))
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, QuestionDTO questiondto)
        {
            try
            {
                if (!_testLogic.ElemExists<Question>(id))
                {
                    return NotFound();
                }
                var quest = await _context.Questions.FindAsync(id);

                quest.Quest = questiondto.Question;
                quest.CorrectAnswer = questiondto.CorrectAnswer;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_testLogic.ElemExists<Question>(id))
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

        // POST: api/Questions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO questiondto)
        {
            var quest = new Question
            {
                Quest = questiondto.Question,
                CorrectAnswer = questiondto.CorrectAnswer,
                TestId = questiondto.TestId,
                
            };
            //var test = await _context.Tests.Include(e => e.Questions).FirstOrDefaultAsync(q => q.TestId == questiondto.TestId);
            _context.Questions.Add(quest);
            await _context.SaveChangesAsync();

            return Ok(quest);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionDTO>> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (!_testLogic.ElemExists<Question>(id))
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return ModelsToDto.QuestToDTO(question);
        }
    }
}
