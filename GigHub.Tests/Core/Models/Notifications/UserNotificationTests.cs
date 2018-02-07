using GigHub.Core.Models;
using GigHub.Core.Models.Notifications;
using NUnit.Framework;
using System;

namespace GigHub.Tests.Core.Models.Notifications
{
    [TestFixture]
    public class UserNotificationTests
    {
        [Test]
        public void MarkAsRead_WhenCalled_SetsIsReadToTrue()
        {
            // Arrange
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCancelled);
            var userNotification = new UserNotification(new ApplicationUser(), notification);

            // Act
            userNotification.MarkAsRead();

            // Assert
            Assert.AreEqual(true, userNotification.IsRead);
        }

        [Test]
        public void Constructor_WhenCalledWithInvalidArguments_ThrowsErrors()
        {
            // Arrange
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCancelled);

            // Assert
            Assert.That(() => new UserNotification(null, notification),
                Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => new UserNotification(new ApplicationUser(), null),
                Throws.TypeOf<ArgumentNullException>());
        }
    }
}
