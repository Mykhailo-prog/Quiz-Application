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
using QuizProject.Services.DataTransferService;
using QuizProject.Services.RepositoryService;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly ILogger<TestsController> _logger;
        private readonly ITestRepository<Test, TestDTO> _repository;

        public TestsController(RepositoryFactory factory, ILogger<TestsController> logger)
        {
            _repository = factory.GetRepository<ITestRepository<Test, TestDTO>>();
            _logger = logger;
        }

        // GET: api/Tests
        [HttpGet]
        
        public async Task<IEnumerable<Test>> GetTests()
        {
            return await _repository.GetAll();
        }

        // GET: api/Tests/id
        [HttpGet("id")]
        public async Task<ActionResult<Test>> GetTest(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var test = await _repository.GetByID(int.Parse(id));

            if (test == null)
            {
                return NotFound("No test with this Id");
            }

            return Ok(test);
        }

        // PUT: api/Tests
        [HttpPut]
        public async Task<IActionResult> PutTest(string id, TestDTO editTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound("Incorrect Id");
            }

            var result = await _repository.Update(int.Parse(id), editTest);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST: api/Tests
        [HttpPost]
        public async Task<IActionResult> PostTest(string id, TestDTO testdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound("Incorrect Id");
            }

            var result = await _repository.Create(int.Parse(id),testdto);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        // DELETE: api/Tests/id
        [HttpDelete]
        public async Task<IActionResult> DeleteTest(string id)
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
