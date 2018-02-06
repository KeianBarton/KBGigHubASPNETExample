using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<ApplicationUser> GetFollowers(string userId);
        Following GetFollowing(string userId, string artistId);
        IEnumerable<Following> GetFollowingForUser(string userId);
        void Add(Following following);
        void Remove(Following following);
    }
}