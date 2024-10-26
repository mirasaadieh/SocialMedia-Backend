using Microsoft.AspNetCore.SignalR;

namespace SocialMedia
{
    public class PostHub : Hub
    {
        public async Task BroadcastLikeCount(int postId, int likeCount)
        {
            await Clients.All.SendAsync("ReceiveLikeCountUpdate", postId, likeCount);
        }
        public async Task BroadcastCommentCount(int postId, int commentCount)
        {
            await Clients.All.SendAsync("ReceiveCommentCountUpdate", postId, commentCount);
        }
    }
}
