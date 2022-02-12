using Microsoft.AspNetCore.Mvc;
using Recodo.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Recodo.API.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("api/TestOkResponse")]
        public IActionResult TestOkResponse()
        {
            List<UserDTO> list = new List<UserDTO>();
            list.Add(new UserDTO() { Id = 1, Email = "user1@mail.com", UserName = "user" });
            list.Add(new UserDTO() { Id = 2, Email = "user2@mail.com", UserName = "user2" });
            list.Add(new UserDTO() { Id = 3, Email = "user3@mail.com", UserName = "user3" });

            return Ok(list);
        }

        [HttpGet]
        [Route("api/TestExceptionResponse")]
        public IActionResult TestExceptionResponse()
        {
            throw new Exception("Test Exception");
        }

        [HttpPost]
        [Route("api/TestValidationResponse")]
        public IActionResult TestValidationResponse([FromBody] NewUserDTO user)
        {
            return Ok("TestValidation for NewUserDTO");
        }
    }
}
