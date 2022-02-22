using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Comment;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Collections.Generic;

namespace Recodo.BLL.Services
{
    public sealed class CommentService : BaseService
    {
        public CommentService(RecodoDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<CommentDTO> CreateComment(NewCommentDTO newComment)
        {
            var commentEntity = _mapper.Map<Comment>(newComment);

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();

            var createdComment = await _context.Comments
                .Include(comment => comment.Author)
                    .ThenInclude(user => user.AvatarLink)
                .FirstAsync(comment => comment.Id == commentEntity.Id);

            return _mapper.Map<CommentDTO>(createdComment);
        }

        public async Task DeleteComment(int commentId)
        {
            var commentEntity = await _context.Comments.FirstOrDefaultAsync(p => p.Id == commentId);
            _context.Comments.Update(commentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(CommentDTO commentDto)
        {
            var commentEntity = await _context.Comments.FirstOrDefaultAsync(p => p.Id == commentDto.Id);

            commentEntity.Body = commentDto.Body;

            _context.Comments.Update(commentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CommentDTO>> GetAllComments()
        {
            var allComments = await _context.Comments.ToListAsync();
            var allCommentsDTO = _mapper.Map<List<Comment>, List<CommentDTO>>(allComments);
            return allCommentsDTO;
        }
    }
}
