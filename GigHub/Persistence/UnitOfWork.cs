using GigHub.Models;
using GigHub.Repositories;
using System.Threading.Tasks;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
            Followings = new FollowingRepository(context);
            Attendances = new AttendanceRepository(context);
            Users = new UserRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}