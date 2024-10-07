using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Repository
{
    public class FollowerRepository : IFollower
    {
        private readonly DataContext _context;
        public FollowerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFollower(Follower follower)
        {
            _context.Add(follower);
            return Save();
        }

        public bool FollowerExists(int id)
        {
            return _context.Followers.Any(f => f.FollowerId == id);
        }

        public Follower GetFollower(int id)
        {
            var follower =  _context.Followers.Where(f => f.FollowerId == id).FirstOrDefault();
            if (follower == null)
            {
                throw new Exception("Follower not found.");
            }
            return follower;
        }

        public ICollection<Follower> GetFollowers()
        {
            return _context.Followers.ToList();
        }

        public async Task<bool> IsFollowing(int followerUserId, int userId)
        {
            return await _context.Followers.AnyAsync(f => f.FollowerUserId == followerUserId && f.UserId == userId);            
        }

        public bool RemoveFollower(Follower follower)
        {
            _context.Remove(follower);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
