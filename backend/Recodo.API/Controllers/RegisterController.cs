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
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public RegisterController(AuthService authService, UserService userService)
        {
            this._authService = authService;
            this._userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthUserDTO>> Post([FromBody] UserRegisterDTO userDTO)
        {
            var createdUser = await _userService.CreateUser(userDTO);

            var accessToken = await _authService.GenerateAccessToken(createdUser.Id, createdUser.UserName, createdUser.Email);

            var result = new AuthUserDTO 
            { 
                Token = accessToken, 
                User = createdUser 
            };

            return new JsonResult(result);

        }
    }
}
