using Microsoft.AspNetCore.SignalR;
using Recodo.Common.Dtos.Reactions;
using System.Threading.Tasks;

namespace Recodo.BLL.Hubs
{
    public sealed class CommentReactionHub : Hub
    {
        public async Task Send(CommentReactionDTO reaction)
        {
            await Clients.All.SendAsync("NewVideoReaction", reaction);
        }
    }
}
