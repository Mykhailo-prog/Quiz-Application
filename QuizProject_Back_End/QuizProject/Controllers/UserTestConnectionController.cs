using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizProject.Models;
using QuizProject.Models.DTO;
using QuizProject.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserTestConnectionController : ControllerBase
    {
        private readonly IDataTransferServise _dto;
        private readonly QuizContext _context;
        private readonly ILogger<UserTestConnectionController> _logger;

        public UserTestConnectionController(QuizContext context, IDataTransferServise dto, ILogger<UserTestConnectionController> logger)
        {
            _context = context;
            _dto = dto;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCreatedTest>>> GetTestConnection()
        {
            return await _context.CreatedTests.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<UserCreatedTest>> PostTestConnection(CreatedTestDTO ctdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uct = new UserCreatedTest
            {
                QuizUserId = ctdto.UserId,
                TestId = ctdto.TestId,
            };

            _context.CreatedTests.Add(uct);
            await _context.SaveChangesAsync();

            return Ok(_dto.CreatedTestToDTO(uct));
        }
        [HttpPut]
        public async Task<IActionResult> PutTestConnection(string id, CreatedTestDTO ctdto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var uct = await _context.CreatedTests.FindAsync(int.Parse(id));

                if(uct == null)
                {
                    return NotFound("No user-test with this Id");
                }

                uct.TestId = ctdto.TestId;
                uct.QuizUserId = ctdto.UserId;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err)
            {
                _logger.LogError("Db update error!\n{0}", err.Message);
                return BadRequest(err.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Database wasn't updated. Error on updating Test Connection with id:{0} \nReason: {1}", id, e.Message);
                return BadRequest(e.Message);
            }

            //TODO: I think it is not the best type to return if everything ok, it should be Ok(); :) \
            //DONE
            return Ok();
        }
    }
}
