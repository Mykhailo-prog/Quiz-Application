using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.RepositoryService.RepositoryAbstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Repositories
{
    public class QuestionRepository : DefaultRepositoryAbstraction<Question, QuestionDTO>
    {
        public QuestionRepository(QuizContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Question>> GetAll()
        {
            return await _dbSet.Include(q => q.Answers).ToListAsync();
        }

        public override async Task<Question> GetByID(int id)
        {
            await _context.Answers.LoadAsync();

            var quest = await _dbSet.FindAsync(id);

            if(quest == null)
            {
                return null;
            }

            return quest;
        }

        public override async Task<UserManagerResponse> Create(QuestionDTO item)
        {
            try
            {
                var quest = new Question
                {
                    Quest = item.Question,
                    CorrectAnswer = item.CorrectAnswer,
                    TestId = item.TestId,
                };

                await _dbSet.AddAsync(quest);
                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Question have been created successfully!"
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Creating question proccess failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public override async Task<UserManagerResponse> Update(int id, QuestionDTO item)
        {
            try
            {
                var quest = await _dbSet.FindAsync(id);

                if (quest == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Questiong updating failed",
                        Errors = new List<string> { "Question not found" }
                    };
                }


                quest.Quest = item.Question;
                quest.CorrectAnswer = item.CorrectAnswer;

                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Question updated successfully"
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Question updating failed",
                    Errors = new List<string> { e.Message }
                };
            }
        }
    }
}
