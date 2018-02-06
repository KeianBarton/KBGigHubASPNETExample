using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            // Property configurations
            HasKey(f => f.FollowerId);
            Property(f => f.FollowerId)
                .HasColumnOrder(1);

            HasKey(f => f.FolloweeId);
            Property(f => f.FolloweeId)
                .HasColumnOrder(2);

            // Relationship configurations
        }
    }
}