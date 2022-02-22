using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Recodo.API.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Recodo.BlobAPI.Controllers
{
    [Route("api/Blob")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _blobService;

        public FileController(IFileService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetBlobs()
        {
            return Ok(await _blobService.ListAsync());
        }

        [HttpGet]
        public async Task<FileStreamResult> GetFile(int id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            return File(await _blobService.DownloadAsync(id, accessToken), "application/mp4", id.ToString()+".mp4");
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 157286400)]
        public async Task<IActionResult> UploadFile(int id, IFormFile file)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            await _blobService.UploadAsync(file, accessToken.FirstOrDefault());

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(int id)
        {
            await _blobService.DeleteAsync(id);

            return NoContent();
        }
    }
}
