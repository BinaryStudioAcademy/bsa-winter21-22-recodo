using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Services;
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
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> loadingFile(int id)
        {
            return Ok(await _fileService.SaveVideo(id));
        }
    }
}
