using GigHub.Repositories;
using System.Threading.Tasks;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        IAttendanceRepository Attendances { get; }
        INotificationRepository Notifications { get; }
        IUserRepository Users { get; }

        void Complete();
        Task<int> CompleteAsync();
    }
}
