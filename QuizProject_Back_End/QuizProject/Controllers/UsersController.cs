using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using QuizProject.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Microsoft.Extensions.Logging;
using QuizProject.Services.DataTransferService;
using QuizProject.Services.AuthService;
using QuizProject.Services.RepositoryService;
using QuizProject.Services.AdministratorService;
using QuizProject.Models.Entity;
using QuizProject.Models.ResponseModels;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAdministratorService _adminService;
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository<QuizUser, UserDTO> _repository;

        public UsersController(RepositoryFactory factory, ILogger<UsersController> logger, IAuthService authService, IAdministratorService adminService)
        {
            _authService = authService;
            _logger = logger;
            _repository = factory.GetRepository<IUserRepository<QuizUser, UserDTO>>();
            _adminService = adminService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<QuizUser>> GetUsers()
        {
            return await _repository.GetAll();
        }

        // GET: api/Users/id
        [HttpGet("id")]
        public async Task<ActionResult<QuizUser>> GetUser([FromQuery] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            var user = await _repository.GetByID(int.Parse(id));

            if (user == null)
            {
                return NotFound("No user with this Id");
            }

            return Ok(user);
        }

        // POST: api/users/checkrole
        [HttpPost("checkrole")]
        public async Task<bool> CheckRole([FromQuery] string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }
            var result = await _authService.CheckRole(login);

            if (result.Success)
            {
                return true;
            }

            return false;
        }

        // POST: api/Users/resetscore
        [HttpPost("resetscore")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetUserScore([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Incorrect login");
            }

            var result = await _adminService.ResetScore(name);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);

        }

        // POST: api/Users/changepass
        [HttpPost("changepass")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminChangePassword([FromQuery] string name, [FromQuery] string password)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Incorrect name or password ");
            }

            var result = await _adminService.ChangePassword(name, password);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST: api/Users/adminconfirm
        [HttpPost("adminconfirm")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminConfirmEmail([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Incorrect login");
            }

            var result = await _adminService.ConfirmEmail(name);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        // PUT: api/Users
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutUser([FromQuery] string id, [FromBody] UserUpdateDTO userUpdto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incorrect Id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.UpdateScore(int.Parse(id), userUpdto) as UserManagerResponse<FinishTestResponse>;

            if (!result.Success)
            {
                foreach(var e in result.Errors)
                {
                    _logger.LogError("Error : {0}", e);
                }
                return BadRequest(result);
            }

            return Ok(result.Object);
        }

        // POST: api/Users
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<QuizUser>> PostUser([FromBody] UserDTO userdto)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.Create(userdto);

            if (!result.Success)
            {
                _logger.LogError("Error : {0}", result.Errors.FirstOrDefault());
                return BadRequest(result);
            }

            return Ok(result);
        }

        // DELETE: api/Users
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Incorrect login");
            }
            
            var result = await _repository.Delete(name);

            if (!result.Success)
            {
                foreach (var e in result.Errors)
                {
                    _logger.LogError("Error : {0}", e);
                }
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
