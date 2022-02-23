﻿using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Exceptions;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.Auth;
using Recodo.Common.Dtos.User;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthUserDTO>> Register([FromBody] NewUserDTO userDTO)
        {
            var createdUser = await _userService.CreateUser(userDTO);

            var token = await _authService.GenerateAccessToken(createdUser.Id, createdUser.WorkspaceName, createdUser.Email);

            var result = new AuthUserDTO
            {
                Token = token,
                User = createdUser
            };

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthUserDTO>> Login([FromBody] LoginUserDTO userDTO)
        {
            return Ok(await _authService.Authorize(userDTO));
        }

        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] ExternalAuthDto externalAuth)
        {
            var payload = await _authService.VerifyGoogleToken(externalAuth);
            if (payload == null)
            {
                throw new VerifyGoogleTokenException();
            }

            var createdUser = await _userService.CreateGoogleUser(externalAuth, payload);
            var token = await _authService.GenerateAccessToken(createdUser.Id, createdUser.WorkspaceName, createdUser.Email);

            var result = new AuthUserDTO
            {
                Token = token,
                User = createdUser
            };

            return new JsonResult(result);
        }
    }
}
