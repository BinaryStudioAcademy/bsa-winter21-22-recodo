using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Recodo.API.BLL.Interfaces;
using Recodo.FileAPI.Dtos;
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

        [HttpGet]
        public async Task<IActionResult> GetFile(int id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var (response, errorCode) = await _blobService.DownloadAsync(id, accessToken);
            if (errorCode.HasValue)
                return Unauthorized();
            return Ok(File(response, "application/mp4", id.ToString() + ".mp4"));
        }

        [HttpGet("GetUrl")]
        public async Task<ActionResult<FileDto>> GetFileUrl(int id)
        {
            var token = Request.Headers[HeaderNames.Authorization];

            var response = await _blobService.GetUrlAsync(id, token.ToString());

            return Ok(new FileDto(response));
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        public async Task<IActionResult> UploadFile()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            int videoId = 0;
            try
            {
                var headerResult = Request.Headers["videoId"].First().ToString();
                videoId = Convert.ToInt32(headerResult);
            }
            catch
            {
                return BadRequest();
            }

            Stream file = Request.Body;
            var responseId = await _blobService.UploadAsync(file, accessToken, videoId);
            if (responseId != null)
            {
                return Ok(responseId);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(int id)
        {
            await _blobService.DeleteAsync(id);

            return NoContent();
        }
    }
}
