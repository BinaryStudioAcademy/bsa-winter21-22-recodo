using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Reactions;
using Recodo.Common.Enums;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using Microsoft.AspNetCore.SignalR;
using Recodo.BLL.Hubs;

namespace Recodo.BLL.Services
{
    public sealed class ReactionService : BaseService
    {
        private readonly IHubContext<CommentReactionHub> _commentHub;
        private readonly IHubContext<VideoReactionHub> _videoHub;        
        public ReactionService(RecodoDbContext context, IMapper mapper, IHubContext<CommentReactionHub> commentHub, IHubContext<VideoReactionHub> videoHub) : base(context, mapper) 
        {
            _commentHub = commentHub;
            _videoHub = videoHub;
        }

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
            await _videoHub.Clients.All.SendAsync("NewReaction", createdReactionDTO);
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
            await _videoHub.Clients.All.SendAsync("NewReaction", createdReactionDTO);
        }
    }
}
