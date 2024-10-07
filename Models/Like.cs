using SocialMedia.Entity.Views;

namespace SocialMedia.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        //public ICollection<UsersLikes> UsersLikes { get; set; }
        //public ICollection<PostsLikes> PostsLikes { get; set; }
        //public ICollection<CommentsLikes> CommentsLikes { get; set; }

    }
}
