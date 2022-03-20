using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Services;
using Recodo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoService _videoService;
        public VideoController(VideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDTO>> GetVideoById(int id)
        {
            return Ok(await _videoService.GetVideoById(id));
        }
        
        [HttpGet("{id:int}/videos")]
        public async Task<ActionResult<List<VideoDTO>>> GetVideoByFolderId(int id)
        {
            return Ok(await _videoService.GetVideosByFolderId(id));
        }

        [HttpGet("user/{id:int}")]
        public async Task<ActionResult<List<VideoDTO>>> GetVideosByUserIdWithoutFolder(int id)
        {
            return Ok(await _videoService.GetVideosByUserIdWithoutFolder(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _videoService.Delete(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VideoDTO video)
        {
            await _videoService.Update(video);
            return NoContent();
        }
    }
}
