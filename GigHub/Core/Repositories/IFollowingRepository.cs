using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
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