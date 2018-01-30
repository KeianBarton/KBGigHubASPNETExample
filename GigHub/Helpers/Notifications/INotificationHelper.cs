using GigHub.Models;
using GigHub.Models.Notifications;
using System.Collections.Generic;

namespace GigHub.Helpers.Notifications
{
    public interface INotificationHelper
    {
        void AddUserNotifications(
            Gig gig,
            IEnumerable<ApplicationUser> users,
            NotificationType notificationType);
    }
}