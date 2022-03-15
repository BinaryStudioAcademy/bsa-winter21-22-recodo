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
    public class VideosController : ControllerBase
    {
        private readonly VideoService _videoService;
        public VideosController(VideoService videoService)
        {
            _videoService = videoService;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<VideoDTO>>> GetVideoByFolderId(int id)
        {
            return Ok(await _videoService.GetVideosByFolderId(id));
        }
    }
}
