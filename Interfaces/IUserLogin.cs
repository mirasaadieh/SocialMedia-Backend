using SocialMedia.Entity.Views;
using SocialMedia.Models;

namespace SocialMedia.Interfaces
{
    public interface IUserLogin
    {
        public ICollection<UserLogin> GetUsers();
        public ICollection<UserLogin> GetOtherUsers(int loggedInUserId);
        public ICollection<UsersPosts> GetOtherUsersPosts(int loggedInUserId);

        public UserLogin GetUser(int id);
        public bool UserExists(int id);
        public IEnumerable<UserLogin> SearchUsers(string searchTerm);
        public ICollection<UsersPosts> GetPostByUserId(int userId);
        public Task<int> NbOfPostsByUserId(int userId);
        public Task<int> NbOfFollowersByUserId(int userId);
        public ICollection<UsersLikes> GetLikesByUserId(int userId);
        public ICollection<UsersComments> GetCommentsByUserId(int userId);
        public ICollection<UsersFollowers> GetFollowersByUserId(int userId);
        public Task<UserLogin> ValidateUserAsync(string userName, string password);
        bool CreateUser(UserLogin user);
        bool DeleteUser(UserLogin user);

        bool UpdateUser(UserLogin user);
        bool Save();
    }
}
