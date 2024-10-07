using SocialMedia.Data;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
    public class CommentRepository : IComments
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CommentExists(int id)
        {
            return _context.Comments.Any(u => u.Id == id);
        }

        public bool Create(Comment comment)
        {
            _context.Add(comment);
            return Save();
        }

        public Comment GetComment(int id)
        {
            return _context.Comments.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Comment> GetComments()
        {
            return _context.Comments.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
