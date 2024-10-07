using SocialMedia.Entity.Views;

namespace SocialMedia.Models
{
    public class Follower
    {
        public int FollowerId { get; set; }
        public int UserId { get; set; }
        public int FollowerUserId { get; set; }
        public string FollowerUserName { get; set; }
        public DateTime FollowedAt { get; set; }

    }
}
