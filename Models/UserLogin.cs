using SocialMedia.Entity.Views;

namespace SocialMedia.Models
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImg { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Follower>? Followers { get; set; }
        //public ICollection<UsersPosts> UsersPosts { get; set; }
        //public ICollection<UsersLikes> UsersLikes { get; set; }
        //public ICollection<UsersFollowers> UsersFollowers { get; set; }
        //public ICollection<UsersComments> UsersComments { get; set; }



    }
}
