using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
    public class LikeRepository : ILikes
    {
        private readonly DataContext _context;
        public LikeRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateLike(Like like)
        {
            _context.Add(like);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Like> GetLikes()
        {
            return _context.Likes.ToList();
        }

        public bool RemoveLike(Like like)
        {
            _context.Remove(like);
            return Save();
        }

        public bool LikeExists(int id)
        {
            return _context.Likes.Any(l => l.Id == id);
        }

        public Like GetLike(int id)
        {
            return _context.Likes.Where(l => l.Id == id).FirstOrDefault();
        }
    }
}
