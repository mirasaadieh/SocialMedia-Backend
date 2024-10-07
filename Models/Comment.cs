using SocialMedia.Entity.Views;

namespace SocialMedia.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        //public ICollection<UsersComments> UsersComments { get; set; }
        //public ICollection<PostsComments> PostsComments { get; set; }
        //public ICollection<CommentsLikes> CommentsLikes { get; set; }


    }
}
