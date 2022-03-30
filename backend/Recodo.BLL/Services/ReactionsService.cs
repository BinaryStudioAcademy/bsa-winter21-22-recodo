using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Reactions;
using Recodo.Common.Enums;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;

namespace Recodo.BLL.Services
{
    public sealed class ReactionService : BaseService
    {
        public ReactionService(RecodoDbContext context, IMapper mapper) : base(context, mapper) 
        { }

        public async Task ReactVideo(NewVideoReactionDTO reaction)
        {
            var video = await _context.Videos.FindAsync(reaction.VideoId);
            var userReaction = video.Reactions.FirstOrDefault(x => x.UserId == reaction.UserId);
            if (userReaction != null)
            {
                video.Reactions.Remove(userReaction);
                await _context.SaveChangesAsync();
                if (userReaction.Reaction == reaction.Reaction)
                {
                    return;
                }
            }

            var newReaction = _mapper.Map<VideoReaction>(reaction);
            video.Reactions.Add(newReaction);
            await _context.SaveChangesAsync();
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
