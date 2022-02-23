using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Recodo.BLL.Services;
using System;
using System.Linq;
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
        public async Task<IActionResult> SaveFile(int id)
        {
            return Ok(await _fileService.SaveVideo(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _fileService.GetFile(Convert.ToInt32(userId), id);
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
