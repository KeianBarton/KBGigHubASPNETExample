﻿using GigHub.Core.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancelled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public Gig(IEnumerable<ApplicationUser> followers) : this()
        {
            var notification = Notification.FactoryGig(this, NotificationType.GigCreated);

            foreach (var follower in followers)
            {
                follower.Notify(notification);
            }
        }

        public void Cancel()
        {
            IsCancelled = true;

            var notification = Notification.FactoryGig(this, NotificationType.GigCancelled);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.FactoryGig(this, NotificationType.GigUpdated, DateTime, Venue);
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