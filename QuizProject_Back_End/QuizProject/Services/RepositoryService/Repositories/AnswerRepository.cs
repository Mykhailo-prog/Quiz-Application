using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Models.Entity;
using QuizProject.Models.ResponseModels;
using QuizProject.Services.RepositoryService.RepositoryAbstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Repositories
{
    public class AnswerRepository : AnswerRepositoryAbstraction<Answer, AnswerDTO>
    {
        public AnswerRepository(QuizContext context) : base(context)
        {
        }

        public async override Task<UserManagerResponse> Create(List<AnswerDTO> items)
        {
            try
            {
                foreach (AnswerDTO item in items)
                {
                    var answer = new Answer
                    {
                        Ans = item.Answer,
                        QuestionId = item.QuestionId,
                    };
                    await _dbSet.AddAsync(answer);
                }

                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "All answers have been created successfully!"
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Creating answers proccess failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public override async Task<UserManagerResponse> Update(int id, AnswerDTO item)
        {
            try
            {
                var answer = await _dbSet.FindAsync(id);

                if (answer == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Answer updating failed",
                        Errors = new List<string> { "Answer not found" }
                    };
                }


                answer.Ans = item.Answer;
                answer.QuestionId = item.QuestionId;

                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Answer updated successfully"
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Answer updating failed",
                    Errors = new List<string> { e.Message }
                };
            }
        }
    }
}
