using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUser(string userId);
    }
}