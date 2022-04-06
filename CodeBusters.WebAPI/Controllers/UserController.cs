using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Token;
using CodeBusters.Models.Users;
using CodeBusters.Services.Token;
using CodeBusters.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBusters.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ITokenService _tokenService;
        public UserController(IUserService service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }
        // [Authorize]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _service.RegisterUserAsync(model);
            if (registerResult)
            {
                return Ok("User was registered.");
            }

            return BadRequest("User could not be registered.");
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            var userDetail = await _service.GetUserByIdAsync(userId);
            if (userDetail is null)
            {
                return NotFound();
            }
            return Ok(userDetail);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            return await _service.UpdateUserAsync(request)
                ? Ok("User updated successfully.")
                : BadRequest("User could not be updated.");
        }

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> ChangeAdminStatus([FromRoute] int userId)
        {
            return await _service.ChangeAdminStatusAsync(userId) 
                ? Ok($"Admin status changed for User {userId}.")
                : BadRequest($"Unable to change admin status for User {userId}.");
        }
        
        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            return await _service.DeleteUserAsync(userId) 
                ? Ok($"User {userId} was deleted successfully.")
                : BadRequest($"User {userId} not found or could not be deleted.");

        }
      
      // Jwt Tokens
      [HttpPost("~/api/Token")]
        public async Task<IActionResult> Token([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokenResponse = await _tokenService.GetTokenAsync(request);
            if (tokenResponse is null)
                return BadRequest("Invalid username or password.");

            return Ok(tokenResponse);
        }

    }
}