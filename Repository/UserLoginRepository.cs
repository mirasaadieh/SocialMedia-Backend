using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Entity.Views;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
    public class UserLoginRepository : IUserLogin
    {
        private readonly DataContext _context;
        public UserLoginRepository(DataContext context )
        {
            _context = context;
        }       

        public ICollection<UserLogin> GetUsers() {
            return _context.Users.ToList();
        }
        public UserLogin GetUser(int id) {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
        public ICollection<UsersPosts> GetPostByUserId(int userId)
        {
            var test= _context.UsersPosts.AsQueryable().Where(up => up.UserId == userId).ToList();
            return test;
        }
        public bool CreateUser(UserLogin user)
        {
           _context.Add(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<UsersLikes> GetLikesByUserId(int userId)
        {
            return _context.UsersLikes.Where(ul => ul.UserId == userId).ToList();
        }

        public ICollection<UsersComments> GetCommentsByUserId(int userId)
        {
            return _context.UsersComments.Where(uc => uc.UserId == userId).ToList();
        }
        public ICollection<UsersFollowers> GetFollowersByUserId(int userId)
        {
            return _context.UsersFollowers.Where(uf => uf.Id == userId).ToList();
        }

        public bool UpdateUser(UserLogin user)
        {
            _context.Update(user);
            return Save();          
        }
        public bool DeleteUser(UserLogin user)
        {
            _context.Remove(user);
            return Save();
        }
        public async Task<UserLogin> ValidateUserAsync(string userName, string password)
        {
            // Query the database to find the user by username
            var user = await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

            if (user != null && user.Password == password)
            {
                return user; // Valid user
            }
            return null; // Invalid credentials
        }
        public async Task<int> NbOfPostsByUserId(int userId)
        {
            int count = await _context.Posts.Where(u => u.UserId == userId).CountAsync();
            return count;
        }

        public async Task<int> NbOfFollowersByUserId(int userId)
        {
            int count = await _context.Followers.Where(u => u.UserId == userId).CountAsync();
            return count;
        }

        public IEnumerable<UserLogin> SearchUsers(string searchTerm)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();  // Convert search term to lowercase
                query = query.Where(user => user.UserName.ToLower().Contains(searchTerm));
            }

            return query.ToList();
            //return _context.Users.FromSqlRaw("SELECT * FROM Users WHERE UserName LIKE {0} OR Email LIKE {0}", $"%{searchTerm}%").ToList();
        }
        public ICollection<UsersPosts> GetOtherUsersPosts(int loggedInUserId)
        {
            // Get all posts except the ones created by the logged-in user
            return _context.UsersPosts.Where(post => post.UserId != loggedInUserId).ToList();

        }
        public ICollection<UserLogin> GetOtherUsers(int loggedInUserId)
        {
            return _context.Users.Where(user => user.Id != loggedInUserId).ToList();

        }
    }
}
