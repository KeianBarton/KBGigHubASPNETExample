using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
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

        public Following GetFollowing(string followerId, string followeeId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }

        public IEnumerable<Following> GetFollowingForUser(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Include(f => f.Follower)
                .Include(f => f.Followee)
                .ToList();
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}