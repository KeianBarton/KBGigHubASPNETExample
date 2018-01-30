using GigHub.Models;
using GigHub.Models.Notifications;
using System.Collections.Generic;

namespace GigHub.Helpers.Notifications
{
    public class NotificationHelper : INotificationHelper
    {
        public void AddUserNotifications(
            Gig gig,
            IEnumerable<ApplicationUser> users,
            NotificationType notificationType)
        {
            var notification = new Notification(gig, notificationType);

            foreach (var user in users)
            {
                user.Notify(notification);
            }
        }
    }
}