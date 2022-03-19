﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Recodo.BLL.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile()
        {
            Request.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value);
            var token = value.ToString();
            return Ok(await _fileService.SaveVideo(token));
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);           
            await _fileService.CheckAccessToFile(Convert.ToInt32(userId), id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> FinishLoadingFile(int id)
        {
            await _fileService.FinishLoadingFile(id);
            return Ok();
        }
    }
}
