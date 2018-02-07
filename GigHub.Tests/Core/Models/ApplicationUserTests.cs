using GigHub.Core.Models;
using GigHub.Core.Models.Notifications;
using NUnit.Framework;
using System;
using System.Linq;

namespace GigHub.Tests.Core.Models
{
    [TestFixture]
    public class ApplicationUserTests
    {
        [Test]
        public void Notify_WhenCalled_ShouldAddTheNotification()
        {
            // Arrange
            var user = new ApplicationUser();
            var notification = Notification.FactoryGig(
                new Gig(), NotificationType.GigCreated, DateTime.Now, "Venue 1");

            // Act
            user.Notify(notification);

            // Assert
            var userNotification = user.UserNotifications.First();
            Assert.AreEqual(user, userNotification.User);
            Assert.AreEqual(notification, user.UserNotifications.First().Notification);

        }
    }
}
