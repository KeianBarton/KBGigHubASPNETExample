using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            // Property configurations
            HasKey(f => new { f.FollowerId, f.FolloweeId });

            // Relationship configurations
        }
    }
}