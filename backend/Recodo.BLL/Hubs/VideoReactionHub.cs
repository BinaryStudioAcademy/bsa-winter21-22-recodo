using Microsoft.AspNetCore.SignalR;
using Recodo.Common.Dtos.Reactions;
using System.Threading.Tasks;

namespace Recodo.BLL.Hubs
{
    public sealed class VideoReactionHub : Hub
    {
        public async Task Send(VideoReactionDTO reaction)
        {
            await Clients.All.SendAsync("NewVideoReaction", reaction);
        }
    }
}
