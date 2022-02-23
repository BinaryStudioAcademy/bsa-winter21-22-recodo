using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodo.API.Extensions;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.Comment;
using Recodo.Common.Dtos.Reactions;
using Recodo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController: ControllerBase
    {
        private readonly CommentService _commentService;
        private readonly ReactionService _reactionService;
        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> GetComments()
        {
            return Ok(await _commentService.GetAllComments());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComments([FromBody] CommentDTO comment)
        {
            await _commentService.UpdateComment(comment);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> CreateComment([FromBody] NewCommentDTO comment)
        {
            return Ok(await _commentService.CreateComment(comment));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteComment(id);
            return NoContent();
        }

        [HttpPost("react")]
        public async Task<IActionResult> ReactComment(NewCommentReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();

            await _reactionService.ReactComment(reaction);
            return Ok();
        }
    }
}