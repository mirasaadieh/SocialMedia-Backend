using SocialMedia.Entity.Views;

namespace SocialMedia.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Image { get; set; }
        public string? caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Like>? Likes { get; set; }
        //public ICollection<UsersPosts> UsersPosts { get; set; }
        //public ICollection<PostsLikes> PostsLikes { get; set; }
        //public ICollection<PostsComments> PostsComments { get; set; }
    }
}
