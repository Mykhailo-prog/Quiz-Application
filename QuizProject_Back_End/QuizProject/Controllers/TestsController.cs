using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly IDataTransferServise _dto;
        private readonly QuizContext _context;
        private readonly ILogger<TestsController> _logger;

        public TestsController(QuizContext context, IDataTransferServise dto, ILogger<TestsController> logger)
        {
            _context = context;
            _dto = dto;
            _logger = logger;
        }

        // GET: api/Tests
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            await _context.Statistics.LoadAsync();
            await _context.Questions.Include(q => q.Answers).LoadAsync();
            return  await _context.Tests.Include(t => t.UserCreatedTest).ToListAsync();
        }

        // GET: api/Tests/5
        [HttpGet("id")]
        public async Task<ActionResult<Test>> GetTest(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var test = await _context.Tests.FindAsync(int.Parse(id));

            if (test == null)
            {
                return NotFound("No test with this Id");
            }

            return Ok(test);
        }

        // PUT: api/Tests
        [HttpPut]
        public async Task<IActionResult> PutTest(TestDTO editTest, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var test = await _context.Tests.FindAsync(int.Parse(id));

                if(test == null)
                {
                    return NotFound("No test with this Id");
                }

                await _context.Questions.Include(q => q.Answers).LoadAsync();

                test.Name = editTest.Name;

                for (int i = 0; i < editTest.Questions.Count; i++)
                {
                    test.Questions[i].Quest = editTest.Questions[i].Quest;
                    test.Questions[i].CorrectAnswer = editTest.Questions[i].CorrectAnswer;

                    for (int j = 0; j < editTest.Questions[i].Answers.Count; j++)
                    {
                        test.Questions[i].Answers[j].Ans = editTest.Questions[i].Answers[j].Ans;
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err)
            {
                _logger.LogError("Db update error!\n{0}", err.Message);
                return BadRequest(err.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Database wasn't updated. Error on updating test with id:{0} \nReason: {1}", id, e.Message);
                return BadRequest(e.Message);
            }

            return Ok();
        }

        // POST: api/Tests
        [HttpPost]
        public async Task<IActionResult> PostTest(TestDTO testdto, string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var user = await _context.QuizUsers.FindAsync(int.Parse(id));

            if(user == null)
            {
                return NotFound("No user with this Id");
            }

            try
            {
                if (_context.Tests.Any(t => t.Name == testdto.Name))
                {
                    return BadRequest("Test with this name already exists! Choose another one.");
                }

                var newTest = new Test
                {
                    Name = testdto.Name,
                };

                _context.Tests.Add(newTest);
                await _context.SaveChangesAsync();

                foreach (var quest in testdto.Questions)
                {
                    var newQuest = new Question
                    {
                        TestId = newTest.TestId,
                        Quest = quest.Quest,
                        CorrectAnswer = quest.CorrectAnswer,
                    };

                    _context.Questions.Add(newQuest);
                    await _context.SaveChangesAsync();

                    foreach (var answer in quest.Answers)
                    {
                        var newAnswer = new Answer
                        {
                            QuestionId = newQuest.Id,
                            Ans = answer.Ans
                        };

                        _context.Answers.Add(newAnswer);
                        await _context.SaveChangesAsync();
                    }
                }

                var userConnect = new UserCreatedTest
                {
                    QuizUserId = user.Id,
                    TestId = newTest.TestId,
                };

                _context.CreatedTests.Add(userConnect);
                await _context.SaveChangesAsync();

                var newTestStat = new TestStatistic
                {
                    TestId = newTest.TestId,
                };

                _context.Statistics.Add(newTestStat);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err)
            {
                _logger.LogError("Db update error!\n{0}", err.Message);
                return BadRequest(err.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Database wasn't created. Error on updating test with id:{0} \nReason: {1}", id, e.Message);
                return BadRequest(e.Message);
            }

            return Ok();
        }

        // DELETE: api/Tests/5
        [HttpDelete]
        public async Task<ActionResult<TestDTO>> DeleteTest(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var test = await _context.Tests.FindAsync(int.Parse(id));

            if(test == null)
            {
                return NotFound("No test with this Id");
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return Ok(_dto.TestToDTO(test));
        }
    }
}
