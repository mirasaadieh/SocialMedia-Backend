using Microsoft.AspNetCore.Mvc;
using SocialMedia.Entity.Views;
using SocialMedia.Models;

namespace SocialMedia.Interfaces
{
    public interface IPosts
    {
        public ICollection<UsersPosts> GetPosts();
        //public ICollection<UsersPosts> GetOtherUsersPosts(int loggedInUserId);

        public Post GetPost(int id);
        public bool PostExists(int id);
        public Task<int> CountofLikesByPostId(int postId);
        public Task<int> CountofCommentsByPostId(int postId);
        public ICollection<Comment> GetCommentsByPostId(int id);
        public bool CreatePost(Post post);
        public bool UpdatePost(Post post);
        public bool DeletePost(Post post);
        public bool Save();
    }
}
