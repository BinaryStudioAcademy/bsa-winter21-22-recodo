using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentController: ControllerBase
    {
        private readonly CommentService _commentService;
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
    }
}