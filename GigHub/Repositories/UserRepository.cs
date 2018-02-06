using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }

    }
}