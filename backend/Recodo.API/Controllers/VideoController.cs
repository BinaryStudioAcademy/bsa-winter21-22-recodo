using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodo.API.Extensions;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.Comment;
using Recodo.Common.Dtos.Reactions;
using Recodo.Common.Dtos.Video;
using Recodo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController: ControllerBase
    {
        private readonly VideoService _videoService;
        private readonly ReactionService _reactionService;
        public VideoController(VideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoDTO>>> GetVideo()
        {
            return Ok(await _videoService.GetVideos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDTO>> GetVideoById(int id)
        {
            return Ok(await _videoService.GetVideoById(id));
        }

        [HttpPost("react")]
        public async Task<IActionResult> ReactVideo(NewVideoReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();
            await _reactionService.ReactVideo(reaction);

            return Ok();
        }

        [HttpDelete("react")]
        public async Task<IActionResult> DeleteReaction(NewVideoReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();
            await _reactionService.ReactVideo(reaction);
            return NoContent();
        }

        
    }
}