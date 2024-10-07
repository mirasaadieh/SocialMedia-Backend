using SocialMedia.Models;

namespace SocialMedia.Entity.Views
{
    public class UsersComments
    {
      
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
        public string text { get; set; }

        //public UserLogin User { get; set; }
        //public Comment Comment { get; set; }
    }
}
