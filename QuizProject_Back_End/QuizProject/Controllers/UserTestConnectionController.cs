using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services.DataTransferService;
using QuizProject.Services.RepositoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserTestConnectionController : ControllerBase
    {
        private readonly ILogger<UserTestConnectionController> _logger;
        private readonly ITestConnectionRepository<UserCreatedTest, CreatedTestDTO> _repository;

        public UserTestConnectionController(RepositoryFactory factory, ILogger<UserTestConnectionController> logger)
        {
            _repository = factory.GetRepository<ITestConnectionRepository<UserCreatedTest, CreatedTestDTO>>();
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<UserCreatedTest>> GetTestConnection()
        {
            return await _repository.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> PostTestConnection([FromBody] CreatedTestDTO ctdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.Create(ctdto);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutTestConnection([FromQuery] string id, [FromBody] CreatedTestDTO ctdto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.Update(int.Parse(id), ctdto);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTestConnection([FromQuery] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var result = await _repository.Delete(int.Parse(id));

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
