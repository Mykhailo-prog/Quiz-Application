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
using Microsoft.Extensions.Logging;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IDataTransferServise _dto;
        private readonly QuizContext _context;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(QuizContext context, IDataTransferServise dto, ILogger<QuestionsController> logger)
        {
            _logger = logger;
            _context = context;
            _dto = dto;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return await _context.Questions.Include(q => q.Answers).ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("id")]
        public async Task<ActionResult<Question>> GetQuestion(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var question = await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == int.Parse(id));

            if (question == null)
            {
                return NotFound("No question with this Id");
            }

            return question;
        }

        [HttpPut]
        public async Task<IActionResult> PutQuestion(string id, QuestionDTO questiondto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrct Id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var quest = await _context.Questions.FindAsync(int.Parse(id));

                if(quest == null)
                {
                    return NotFound("No question with this Id");
                }

                quest.Quest = questiondto.Question;
                quest.CorrectAnswer = questiondto.CorrectAnswer;

                await _context.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException err)
            {
                _logger.LogError("Db update error!\n{0}", err.Message);
                return BadRequest(err.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Database wasn't updated. Error on updating question with id:{0} \nReason: {1}", id, e.Message);
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO questiondto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        // DELETE: api/Questions
        [HttpDelete]
        public async Task<ActionResult<QuestionDTO>> DeleteQuestion(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var question = await _context.Questions.FindAsync(int.Parse(id));

            if (question == null)
            {
                return NotFound("No question with this Id");
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return Ok(_dto.QuestToDTO(question));
        }
    }
}
