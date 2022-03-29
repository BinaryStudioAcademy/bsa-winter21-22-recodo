﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Recodo.BLL.Services;
using Recodo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recodo.Common.Dtos.Video;
using Recodo.API.Extensions;

namespace Recodo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly VideoService _videoService;
        public VideoController(VideoService videoService, UserService userService)
        {
            _videoService = videoService;
            _userService = userService;
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

        [HttpPost("share")]
        public async Task<IActionResult> ShareVideo(VideoShareDTO sharePostInfo)
        {
            var user = await _userService.GetUserById(this.GetUserIdFromToken());
            await _videoService.SendEmail(sharePostInfo.Link, sharePostInfo.Email, user.WorkspaceName);
            return Ok();
        }

    }
}
