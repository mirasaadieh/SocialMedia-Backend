using SocialMedia.Models;

namespace SocialMedia.Entity.Views
{
    public class UsersPosts
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string? caption { get; set; }
    }
}
