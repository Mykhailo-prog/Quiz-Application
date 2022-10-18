using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class TestsController : ControllerBase
    {
        private readonly ITestLogic _testLogic;
        private readonly QuizContext _context;

        public TestsController(QuizContext context, ITestLogic testLogic)
        {
            _context = context;
            _testLogic = testLogic;
        }

        // GET: api/Tests
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            _context.Statistics.Load();
            _context.Questions.Include(q => q.Answers).Load();
            return  await _context.Tests.Include(t => t.UserCreatedTest).ToListAsync();
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            if (!_testLogic.ElemExists<Test>(id))
            {
                return NotFound();
            }

            return test;
        }

        // PUT: api/Tests/5
        [HttpPut]
        public async Task<IActionResult> PutTest(TestDTO editTest, int id)
        {
            if(_testLogic.ElemExists<Test>(id))
            {
                try
                {
                    var test = await _context.Tests.FindAsync(id);
                    await _context.Questions.Include(q => q.Answers).LoadAsync();
                    test.Name = editTest.Name;

                    for(int i = 0; i < editTest.Questions.Count; i++)
                    {
                        test.Questions[i].Quest = editTest.Questions[i].Quest;
                        test.Questions[i].CorrectAnswer = editTest.Questions[i].CorrectAnswer;

                        for(int j = 0; j < editTest.Questions[i].Answers.Count; j++)
                        {
                            test.Questions[i].Answers[j].Ans = editTest.Questions[i].Answers[j].Ans;
                        }
                    }

                    await _context.SaveChangesAsync();
                    return StatusCode(200);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return NotFound("No test with this name!");
        }

        // POST: api/Tests
        [HttpPost]
        public async Task<IActionResult> PostTest(TestDTO testdto, int id)
        {
            var user = await _context.QuizUsers.FindAsync(id);

            if(user != null)
            {
                try
                {
                    if( _context.Tests.Any(t => t.Name == testdto.Name))
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

                    return StatusCode(200);
                }
                catch(Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return NotFound("No user with this id");
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TestDTO>> DeleteTest(int id)
        {
            _context.Questions.Include(q => q.Answers).Load();
            var test = await _context.Tests.FirstAsync(t => t.TestId == id);
            if (!_testLogic.ElemExists<Test>(id))
            {
                return NotFound();
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return _testLogic.TestToDTO(test);
        }
    }
}
