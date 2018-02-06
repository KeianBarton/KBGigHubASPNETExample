using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUser(string userId);
    }
}