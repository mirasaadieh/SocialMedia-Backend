using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;

namespace SocialMedia.Interfaces
{
    public interface IFollower
    {
        public ICollection<Follower> GetFollowers();
        public Follower GetFollower(int id);
        public Task<bool> IsFollowing(int followerUserId, int userId);
        public bool FollowerExists(int id);
        public bool CreateFollower(Follower follower);
        public bool RemoveFollower(Follower follower);
        public bool Save();
    }
}
