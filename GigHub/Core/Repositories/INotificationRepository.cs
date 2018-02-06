using GigHub.Core.Models.Notifications;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetUnreadUserNotificationsWithArtist(string userId);
        List<UserNotification> GetUnreadUserNotifications(string userId);
    }
}