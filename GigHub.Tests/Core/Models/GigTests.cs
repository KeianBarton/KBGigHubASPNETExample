using GigHub.Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GigHub.Tests.Core.Models
{
    [TestFixture]
    public class GigTests
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotifyAllFollowers()
        {
            // Arrange
            var follower = new ApplicationUser();
            var listOfFollowers = new List<ApplicationUser>() { follower };

            // Act
            var gig = new Gig(listOfFollowers);

            // Assert
            Assert.AreEqual(1, follower.UserNotifications.Count);
        }

        [Test]
        public void Cancel_WhenCalled_ShouldSetIsCancelledToTrue()
        {
            // Arrange
            var gig = new Gig();

            // Act
            gig.Cancel();

            // Assert
            Assert.IsTrue(gig.IsCancelled);
        }

        [Test]
        public void Cancel_WhenCalled_AllAttendeesShouldBeNotified()
        {
            // Arrange
            var gig = new Gig();
            var user = new ApplicationUser();
            gig.Attendances.Add(new Attendance() { Attendee = user });

            // Act
            gig.Cancel();

            // Assert
            Assert.AreEqual(1, user.UserNotifications.Count);


        }

        [Test]
        public void Modify_WhenCalled_AllAttendeesShouldBeNotified()
        {
            // Arrange
            var gig = new Gig();
            var user = new ApplicationUser();
            gig.Attendances.Add(new Attendance() { Attendee = user });

            // Act
            gig.Modify(DateTime.Now, "foo", 1);

            // Assert
            Assert.AreEqual(1, user.UserNotifications.Count);
        }
    }
}
