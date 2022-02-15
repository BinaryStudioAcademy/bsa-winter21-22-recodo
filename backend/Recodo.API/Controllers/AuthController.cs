using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("api/Register")]
        public async Task<ActionResult<AuthUserDTO>> Register([FromBody] NewUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            var createdUser = await _userService.CreateUser(userDTO);

            var token = await _authService.GenerateAccessToken(createdUser.Id, createdUser.UserName, createdUser.Email);

            var result = new AuthUserDTO 
            { 
                Token = token, 
                User = createdUser 
            };

            return Ok(result);
        }

        [HttpPost("api/Login")]
        public async Task<ActionResult<AuthUserDTO>> Login([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            return Ok(await _authService.Authorize(userDTO));
        }
    }
}
