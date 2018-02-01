using GigHub.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancelled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
        }

        public Gig(IEnumerable<ApplicationUser> followers)
        {
            Attendances = new Collection<Attendance>();

            var notification = Notification.Factory_Gig(this, NotificationType.GigCreated);

            foreach (var follower in followers)
            {
                follower.Notify(notification);
            }
        }

        public void Cancel()
        {
            IsCancelled = true;

            var notification = Notification.Factory_Gig(this, NotificationType.GigCancelled);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.Factory_Gig(this, NotificationType.GigUpdated, DateTime, Venue);
            DateTime = dateTime;
            Venue = venue;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}