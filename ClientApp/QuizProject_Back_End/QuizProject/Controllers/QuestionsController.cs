using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using QuizProject.Services;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly ITestLogic _testLogic;
        private readonly QuizContext _context;

        public QuestionsController(QuizContext context, ITestLogic testLogic)
        {
            _context = context;
            _testLogic = testLogic;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, QuestionDTO questiondto)
        {
            try
            {
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

        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO questiondto)
        {
            var quest = new Question
            {
                Quest = questiondto.Question,
                CorrectAnswer = questiondto.CorrectAnswer,
                TestId = questiondto.TestId,
                
            };
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

            return _testLogic.QuestToDTO(question);
        }
    }
}
