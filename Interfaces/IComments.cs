using SocialMedia.Models;

namespace SocialMedia.Interfaces
{
    public interface IComments
    {
        public ICollection<Comment> GetComments();
        public Comment GetComment(int id);
        public bool CommentExists(int id);
        public bool Create(Comment comment);
        public bool Save();
    }
}
