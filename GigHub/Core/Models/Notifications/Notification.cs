﻿using System;

namespace GigHub.Core.Models.Notifications
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        public Gig Gig { get; private set; }

        protected Notification()
        {
        }

        private Notification(Gig gig, NotificationType notificationType)
        {
            DateTime = DateTime.Now;
            Gig = gig ?? throw new ArgumentNullException("gig");
            Type = notificationType;
        }

        public static Notification FactoryGig(
            Gig gig,
            NotificationType notificationType,
            DateTime? dateTime = null,
            string venue = "")
        {
            switch (notificationType)
            {
                case NotificationType.GigCancelled:
                    return new Notification(gig, NotificationType.GigCancelled);

                case NotificationType.GigCreated:
                    var notification = new Notification(gig, NotificationType.GigCreated)
                    {
                        OriginalDateTime = dateTime,
                        OriginalVenue = venue
                    };
                    return notification;

                case NotificationType.GigUpdated:
                    return new Notification(gig, NotificationType.GigUpdated);

                default:
                    throw new ArgumentException("notificationType");
            }
        }
    }
}