using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.RepositoryService.RepositoryAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Repositories
{
    public class TestRepository : TestRepositoryAbstraction<Test, TestDTO>
    {
        public TestRepository(QuizContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Test>> GetAll()
        {
            await _context.Statistics.LoadAsync();
            await _context.Questions.Include(q => q.Answers).LoadAsync();

            return await _dbSet.Include(t => t.UserCreatedTest).ToListAsync();
        }

        public override async Task<Test> GetByID(int id)
        {
            await _context.Statistics.LoadAsync();
            await _context.Questions.Include(q => q.Answers).LoadAsync();
            await _context.CreatedTests.LoadAsync();

            var test = await _dbSet.FindAsync(id);

            if (test == null)
            {
                return null;
            }

            return test;
        }

        public override async Task<UserManagerResponse> Create(int id, TestDTO item)
        {
            var user = await _context.QuizUsers.FindAsync(id);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Updating test operation failed!",
                    Errors = new List<string> { "Test not Found" }
                };
            }

            try
            {
                if (_dbSet.Any(t => t.Name == item.Name))
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Updating test operation failed!",
                        Errors = new List<string> { "Test with this name already exists" }
                    };
                }

                var newTest = new Test
                {
                    Name = item.Name,
                };

                await _dbSet.AddAsync(newTest);
                Save();

                foreach (var quest in item.Questions)
                {
                    var newQuest = new Question
                    {
                        TestId = newTest.TestId,
                        Quest = quest.Quest,
                        CorrectAnswer = quest.CorrectAnswer,
                    };

                    await _context.Questions.AddAsync(newQuest);
                    Save();

                    foreach (var answer in quest.Answers)
                    {
                        var newAnswer = new Answer
                        {
                            QuestionId = newQuest.Id,
                            Ans = answer.Ans
                        };

                        await _context.Answers.AddAsync(newAnswer);
                        Save();
                    }
                }

                var userConnect = new UserCreatedTest
                {
                    QuizUserId = user.Id,
                    TestId = newTest.TestId,
                };

                await _context.CreatedTests.AddAsync(userConnect);

                var newTestStat = new TestStatistic
                {
                    TestId = newTest.TestId,
                };

                await _context.Statistics.AddAsync(newTestStat);
                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Test has beed updated successfully!",
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Updating test operation failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public override async Task<UserManagerResponse> Update(int id, TestDTO item)
        {
            try
            {
                var test = await _dbSet.FindAsync(id);

                if (test == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Updating test operation failed!",
                        Errors = new List<string> { "Test not Found" }
                    };
                }

                await _context.Questions.Include(q => q.Answers).LoadAsync();

                test.Name = item.Name;

                for (int i = 0; i < item.Questions.Count; i++)
                {
                    test.Questions[i].Quest = item.Questions[i].Quest;
                    test.Questions[i].CorrectAnswer = item.Questions[i].CorrectAnswer;

                    for (int j = 0; j < item.Questions[i].Answers.Count; j++)
                    {
                        test.Questions[i].Answers[j].Ans = item.Questions[i].Answers[j].Ans;
                    }
                }

                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Test has been updated successfully!",
                };

            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Updating test operation failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }
    }
}
