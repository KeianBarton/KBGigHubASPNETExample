using GigHub.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetFollowers(string userId)
        {
            return _context.Followings
                .Where(f => f.FolloweeId == userId)
                .Select(f => f.Follower);
        }

        public Following GetFollowing(string userId, string artistId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }

        public IEnumerable<Following> GetFollowingForUser(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Include(f => f.Follower)
                .Include(f => f.Followee)
                .ToList();
        }
    }
}