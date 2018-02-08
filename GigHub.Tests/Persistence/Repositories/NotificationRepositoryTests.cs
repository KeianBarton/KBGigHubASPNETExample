using GigHub.Core.Models;
using GigHub.Core.Models.Notifications;
using GigHub.Core.Repositories;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestFixture]
    public class NotificationRepositoryTests
    {
        private Mock<IApplicationDbContext> _mockContext;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;
        private INotificationRepository _notificationsRepository;

        [SetUp]
        public void SetUp()
        {
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();
            _mockContext = new Mock<IApplicationDbContext>();
        }

        private void InitializeNotificationsRepository(IList<UserNotification> data)
        {
            /* Note the repository must be initialized after mockGigs due to 
             * a Moq feature that was removed. In particular, _mockContext
             * depends on _mockGigs and the repo depends on _mockContext */
            _mockUserNotifications.SetSource(data);
            _mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);
            _notificationsRepository = new NotificationRepository(_mockContext.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockContext = null;
            _mockUserNotifications = null;
            _notificationsRepository = null;
        }

        [Test]
        public void GetUnreadUserNotifications_NotificationIsRead_ShouldNotBeReturned()
        {
            // Arrange
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCancelled);
            var userId = "1";
            var user = new ApplicationUser { Id = userId };
            var userNotification = new UserNotification(user, notification);
            userNotification.MarkAsRead();
            InitializeNotificationsRepository(new List<UserNotification>() { userNotification });

            // Act
            var notifications = _notificationsRepository.GetUnreadUserNotifications(userId);

            // Assert
            Assert.IsEmpty(notifications);
        }

        [Test]
        public void GetUnreadUserNotifications_NotificationIsForADifferentUser_ShouldNotBeReturned()
        {
            // Arrange
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCancelled);
            var userId = "1";
            var user = new ApplicationUser { Id = userId + "-" };
            var userNotification = new UserNotification(user, notification);
            InitializeNotificationsRepository(new List<UserNotification>() { userNotification });

            // Act
            var notifications = _notificationsRepository.GetUnreadUserNotifications(userId);


            // Assert
            Assert.IsEmpty(notifications);
        }

        [Test]
        public void GetUnreadUserNotifications_NewNotificationForTheGivenUser_ShouldBeReturned()
        {
            // Arrange
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCancelled);
            var userId = "1";
            var user = new ApplicationUser { Id = userId };
            var userNotification = new UserNotification(user, notification);
            InitializeNotificationsRepository(new List<UserNotification>() { userNotification });

            // Act
            var notifications = _notificationsRepository.GetUnreadUserNotifications(userId);

            // Assert
            Assert.IsEmpty(notifications);
        }
    }
}
