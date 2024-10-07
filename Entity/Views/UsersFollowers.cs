using SocialMedia.Models;

namespace SocialMedia.Entity.Views
{
    public class UsersFollowers
    {
       
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public string UserName { get; set; }
        public string FollowerUserName { get; set; }
    }
}
