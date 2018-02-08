using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Models.Notifications;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestFixture]
    public class NotificationsControllerTests
    {
        private NotificationsController _controller;

        private Mock<INotificationRepository> _mockNotificationRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        private const string _UserId = "1";
        private const string _UserName = "user1@domain.com";

        [SetUp]
        public void SetUp()
        {
            _mockNotificationRepository = new Mock<INotificationRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.SetupGet(u => u.Notifications).Returns(_mockNotificationRepository.Object);

            _controller = new NotificationsController(_mockUnitOfWork.Object);
            _controller.MockCurrentUser(_UserId, _UserName);
        }

        [TearDown]
        public void TearDown()
        {
            _controller = null;
            _mockNotificationRepository = null;
            _mockUnitOfWork = null;
        }

        [Test]
        public void MarkAsRead_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var user = new ApplicationUser();
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCancelled);
            var userNotification = new UserNotification(user, notification);
            var userNotifications = new List<UserNotification>() { userNotification };
            _mockNotificationRepository.Setup(r => r.GetUnreadUserNotifications(_UserId))
                .Returns(userNotifications);

            // Act
            var result = _controller.MarkAsRead().Result;

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void GetNotifications_ValidRequest_ShouldReturnNotificationDtos()
        {
            // Arrange
            var user = new ApplicationUser();
            var notification = Notification.FactoryGig(new Gig(), NotificationType.GigCancelled);
            _mockNotificationRepository.Setup(r => r.GetUnreadUserNotificationsWithArtist(_UserId))
                .Returns(new List<Notification>() { notification });

            // Act
            var result = _controller.GetNotifications();

            // Assert
            Assert.IsInstanceOf<IEnumerable<NotificationDto>>(result);
        }
    }
}
