using SocialMedia.Models;

namespace SocialMedia.Entity.Views
{
    public class UsersLikes
    {
       
        public int UserId { get; set; }
        public int LikeId { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
    }
}
