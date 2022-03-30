using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Comment;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Recodo.BLL.Services
{
    public sealed class CommentService : BaseService
    {
        private readonly UserService _userService;
        public CommentService(RecodoDbContext context, IMapper mapper, UserService userService) : base(context, mapper) {
            _userService = userService;
        }

        public async Task<CommentDTO> CreateComment(NewCommentDTO newComment)
        {
            var commentEntity = _mapper.Map<Comment>(newComment);
            
            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();

            var createdComment = await _context.Comments
                .Include(comment => comment.Author)
                .FirstAsync(comment => comment.Id == commentEntity.Id);

            return _mapper.Map<CommentDTO>(createdComment);
        }

        public async Task DeleteComment(int commentId)
        {
            var commentEntity = await _context.Comments.FirstOrDefaultAsync(p => p.Id == commentId);
            _context.Comments.Remove(commentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(CommentDTO commentDto)
        {
            var commentEntity = await _context.Comments.FirstOrDefaultAsync(p => p.Id == commentDto.Id);

            commentEntity.Body = commentDto.Body;

            _context.Comments.Update(commentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CommentDTO>> GetAllVideosComments(int videoId)
        {
            var allComments = await _context.Comments.Where(comment => comment.VideoId == videoId).ToListAsync();
            var allCommentsDTO = _mapper.Map<List<CommentDTO>>(allComments);
            return allCommentsDTO;
        }

        public async Task<List<CommentDTO>> GetAllVideosComments(int videoId)
        {
            var allComments = await _context.Comments.Where(comment => comment.VideoId == videoId).ToListAsync();
            var allCommentsDTO = _mapper.Map<List<CommentDTO>>(allComments);
            return allCommentsDTO;
        }
    }
}
