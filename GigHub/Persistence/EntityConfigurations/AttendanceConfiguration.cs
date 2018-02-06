using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            // Property configurations
            HasKey(a => a.GigId);
            Property(a => a.GigId)
                .HasColumnOrder(1);

            HasKey(a => a.AttendeeId);
            Property(a => a.AttendeeId)
                .HasColumnOrder(2);

            // Relationship configurations
        }
    }
}