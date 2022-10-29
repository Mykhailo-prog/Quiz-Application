using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.RepositoryService.RepositoryAbstractions;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.Repositories
{
    public class TestConnectionRepository : DefaultRepositoryAbstraction<UserCreatedTest, CreatedTestDTO>
    {
        public TestConnectionRepository(QuizContext context) : base(context)
        {
        }

        public override async Task<UserManagerResponse> Create(CreatedTestDTO item)
        {
            try
            {
                var connect = new UserCreatedTest
                {
                    TestId = item.TestId,
                    QuizUserId = item.UserId
                };

                await _dbSet.AddAsync(connect);
                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Test connection have been created successfully!"
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Test connection question proccess failed!",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public override async Task<UserManagerResponse> Update(int id, CreatedTestDTO item)
        {
            try
            {
                var connect = await _dbSet.FindAsync(id);

                if (connect == null)
                {
                    return new UserManagerResponse
                    {
                        Success = false,
                        Message = "Test connection updating failed",
                        Errors = new List<string> { "Test connection not found" }
                    };
                }


                connect.TestId = item.TestId;
                connect.QuizUserId = item.UserId;

                Save();

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Test connection updated successfully"
                };
            }
            catch (Exception e)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Test connection updating failed",
                    Errors = new List<string> { e.Message }
                };
            }
        }
    }
}
