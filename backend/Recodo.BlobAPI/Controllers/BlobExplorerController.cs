using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodo.API.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Recodo.BlobAPI.Controllers
{
    [Route("api/Blob")]
    [ApiController]
    public class BlobExplorerController : ControllerBase
    {
        private readonly IAzureBlobService _blobService;

        public BlobExplorerController(IAzureBlobService blobService)
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
            return File(await _blobService.DownloadAsync(id), "application/mp4", id.ToString()+".mp4");
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 157286400)]
        public async Task<IActionResult> UploadFile(int id, IFormFile file)
        {
            await _blobService.UploadAsync(id, file);

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
