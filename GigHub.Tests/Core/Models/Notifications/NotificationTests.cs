using GigHub.Core.Models;
using GigHub.Core.Models.Notifications;
using NUnit.Framework;
using System;

namespace GigHub.Tests.Core.Models.Notifications
{
    [TestFixture]
    public class NotificationTests
    {
        [Test]
        public void FactoryGig_WhenCalled_SuccessfullyCreatesCancelledNotification()
        {
            // Act
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCancelled);

            // Result
            Assert.AreEqual(notification.Type, NotificationType.GigCancelled);
        }

        [Test]
        public void FactoryGig_WhenCalled_SuccessfullyCreatesCreatedNotification()
        {
            // Act
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCreated);

            // Result
            Assert.AreEqual(notification.Type, NotificationType.GigCreated);
        }

        [Test]
        public void FactoryGig_WhenCalled_SuccessfullyCreatesUpdatedNotification()
        {
            // Act
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigUpdated);

            // Result
            Assert.AreEqual(notification.Type, NotificationType.GigUpdated);
        }

        [Test]
        public void FactoryGig_WhenCalledWithoutGig_ThrowsError()
        {
            // Assert
            Assert.That(() => Notification.FactoryGig(null, NotificationType.GigCreated),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void FactoryGig_WhenCalledWithoutNotificationType_ThrowsError()
        {
            // Assert
            Assert.That(() => Notification.FactoryGig(new Gig(), (NotificationType)999),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
