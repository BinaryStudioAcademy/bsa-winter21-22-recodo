using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Reactions;
using Recodo.Common.Enums;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Recodo.BLL.Services
{
    public sealed class ReactionService : BaseService
    {
        public ReactionService(RecodoDbContext context, IMapper mapper) : base(context, mapper) 
        { }

        public async Task ReactVideo(NewVideoReactionDTO reaction)
        {
            var reactions = _context.VideoReactions.Where(x => x.UserId == reaction.UserId && x.VideoId == reaction.VideoId);

            if (reactions.Any())
            {
                _context.VideoReactions.RemoveRange(reactions);
                await _context.SaveChangesAsync();
                if(reactions.First().Reaction == reaction.Reaction)
                {
                    return;
                }
            }
            var newReaction = new VideoReaction
            {
                VideoId = reaction.VideoId,
                Reaction = reaction.Reaction,
                UserId = reaction.UserId
            };
            _context.VideoReactions.Add(newReaction);
            await _context.SaveChangesAsync();
            var createdReaction = await _context.VideoReactions.FindAsync(newReaction);
            var createdReactionDTO = _mapper.Map<VideoReactionDTO>(createdReaction);            
        }
        
        public async Task ReactComment(NewCommentReactionDTO reaction)
        {
            var reactions = _context.CommentReactions.Where(x => x.UserId == reaction.UserId && x.CommentId == reaction.CommentId);

            if (reactions.Any())
            {
                _context.CommentReactions.RemoveRange(reactions);
                await _context.SaveChangesAsync();
                if (reactions.First().Reaction == reaction.Reaction)
                {
                    return;
                }
            }

            var newReaction = new CommentReaction
            {
                CommentId = reaction.CommentId,
                Reaction = reaction.Reaction,
                UserId = reaction.UserId
            };
            _context.CommentReactions.Add(newReaction);

            await _context.SaveChangesAsync();
            var createdReaction = await _context.CommentReactions.FindAsync(newReaction);
            var createdReactionDTO = _mapper.Map<CommentReactionDTO>(createdReaction);            
        }
    }
}
