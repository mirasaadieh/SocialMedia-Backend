using SocialMedia.Entity.Views;
using SocialMedia.Models;

namespace SocialMedia.Interfaces
{
    public interface ILikes
    {
        public ICollection<Like> GetLikes();
        public Like GetLike(int id);
        public bool LikeExists(int id);
        public bool CreateLike(Like like);
        public bool RemoveLike(Like like);
        public bool Save();
    }
}
