using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Entity.Views;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
    public class PostRepository : IPosts
    {
        private readonly DataContext _context;
        private readonly IHubContext<PostHub> _hubContext;

        public PostRepository(DataContext context, IHubContext<PostHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        public bool CreatePost(Post post)
        {
            _context.Posts.Add(post);
            return Save();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<UsersPosts> GetPosts()
        {
            
           return _context.UsersPosts.ToList();
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }
        public async Task<int> CountofLikesByPostId(int postId)
        {
            int count = await _context.Likes.Where(p =>p.PostId == postId).CountAsync();
            // Broadcast the updated like count to all connected clients via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveLikeCountUpdate", postId, count);
            return count;
        }
        public async Task<int> CountofCommentsByPostId(int postId)
        {
            int count = await _context.Comments.Where(p => p.PostId == postId).CountAsync();
            // Broadcast the updated like count to all connected clients via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveCommentCountUpdate", postId, count);

            return count;
        }
        public bool Save()
        {
            var saved= _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Comment> GetCommentsByPostId(int id)
        {
            return _context.Comments.Where(pc => pc.PostId == id).ToList();
        }

        public bool UpdatePost(Post post)
        {
            _context.Update(post);
            return Save();
        }

        public bool DeletePost(Post post)
        {
            _context.Remove(post);
            return Save();
        }


        //public ICollection<UsersPosts> GetOtherUsersPosts(int loggedInUserId)
        //{
        //    // Get all posts except the ones created by the logged-in user
        //    return _context.UsersPosts.Where(post => post.UserId != loggedInUserId).ToList();

        //}

    }
}
