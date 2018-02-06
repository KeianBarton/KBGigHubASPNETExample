using GigHub.Models.Notifications;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetUnreadUserNotificationsWithArtist(string userId);
        List<UserNotification> GetUnreadUserNotifications(string userId);
    }
}