using GigHub.Models;
using GigHub.Models.Notifications;
using System.Collections.Generic;

namespace GigHub.Helpers.Notifications
{
    public interface INotificationHelper
    {
        void AddUserNotifications(
            ApplicationDbContext context,
            Gig gig,
            IEnumerable<ApplicationUser> users,
            NotificationType notificationType);
    }
}