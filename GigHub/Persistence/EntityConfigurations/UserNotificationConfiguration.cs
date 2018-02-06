using GigHub.Core.Models.Notifications;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            // Property configurations
            HasKey(un => new { un.UserId, un.NotificationId });

            // Relationship configurations
            // Turn off cascade delete between user notifications and users
            // We have one and only one user for each user notification
            // Each user can have many user notifications
            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}