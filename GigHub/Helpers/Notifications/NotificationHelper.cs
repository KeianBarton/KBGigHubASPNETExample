using GigHub.Models;
using GigHub.Models.Notifications;
using System;
using System.Collections.Generic;

namespace GigHub.Helpers.Notifications
{
    public class NotificationHelper : INotificationHelper
    {
        public void AddUserNotifications(
            ApplicationDbContext context,
            Gig gig,
            IEnumerable<ApplicationUser> users,
            NotificationType notificationType)
        {
            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = notificationType
            };

            foreach (var user in users)
            {
                var userNotification = new UserNotification
                {
                    User = user,
                    Notification = notification
                };
                context.UserNotifications.Add(userNotification);
            }
        }
    }
}