using GigHub.Core.Models.Notifications;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            // Property configurations
            HasRequired(g => g.Gig);

            // Relationship configurations
        }
    }
}