using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Recodo.BLL.Services;
using Recodo.Common.Dtos;
using Recodo.Common.Dtos.Reactions;
using Recodo.Common.Dtos.Video;
using Recodo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recodo.Common.Dtos.Reactions;
using Recodo.API.Extensions;
using Recodo.Common.Enums;

namespace Recodo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoService _videoService;
        private readonly ReactionService _reactionService;
        public VideoController(VideoService videoService, ReactionService reactionService)
        {
            _videoService = videoService;
            _reactionService = reactionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDTO>> GetVideoById(int id)
        {
            return Ok(await _videoService.GetVideoById(id));
        }

        [HttpGet("folder/{id:int}")]
        public async Task<ActionResult<List<VideoDTO>>> GetVideoByFolderId(int id)
        {
            return Ok(await _videoService.GetVideosByFolderId(id));
        }

        [HttpGet("user/{id:int}")]
        public async Task<ActionResult<List<VideoDTO>>> GetVideosByUserIdWithoutFolder(int id)
        {
            return Ok(await _videoService.GetVideosByUserIdWithoutFolder(id));
        }

        [HttpGet("check/{id:int}")]
        public async Task<ActionResult> GetFileState(int id)
        {
            return Ok(await _videoService.CheckVideoState(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Request.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value);
            var token = value.ToString();

            await _videoService.Delete(id, token);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateVideoDTO videoDTO)
        {
            await _videoService.Update(videoDTO);
            return NoContent();
        }
        
        [HttpPost("react")]
        public async Task<IActionResult> ReactVideo(NewVideoReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();
            await _reactionService.ReactVideo(reaction);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<VideoDTO>> CreateVideo(NewVideoDTO newVideo)
        {
            var createdVideo = await _videoService.AddVideo(newVideo);
            return Ok(createdVideo);
        }
    }
}
