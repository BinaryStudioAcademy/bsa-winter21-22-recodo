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

        [HttpPost]
        [Route("api/Register")]
        public async Task<ActionResult<AuthUserDTO>> Register([FromBody] NewUserDTO userDTO)
        {
            var createdUser = await _userService.CreateUser(userDTO);

            var token = await _authService.GenerateAccessToken(createdUser.Id, createdUser.UserName, createdUser.Email);

            var result = new AuthUserDTO 
            { 
                Token = token, 
                User = createdUser 
            };

            return new JsonResult(result);

        }
    }
}
