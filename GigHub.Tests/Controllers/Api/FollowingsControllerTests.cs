using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestFixture]
    public class FollowingsControllerTests
    {
        private FollowingsController _controller;

        private Mock<IFollowingRepository> _mockFollowingsRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        private const string _UserId = "1";
        private const string _UserName = "user1@domain.com";

        [SetUp]
        public void Init()
        {
            _mockFollowingsRepository = new Mock<IFollowingRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.SetupGet(u => u.Followings).Returns(_mockFollowingsRepository.Object);

            _controller = new FollowingsController(_mockUnitOfWork.Object);
            _controller.MockCurrentUser(_UserId, _UserName);
        }

        [TearDown]
        public void Dispose()
        {
            _controller = null;
            _mockFollowingsRepository = null;
            _mockUnitOfWork = null;
        }

        [Test]
        public void Follow_AttemptingToReFollow_ShouldReturnBadRequest()
        {
            // Arrange
            var followingDto = new FollowingDto { FolloweeId = _UserId + "-" };
            var following = new Following();
            _mockFollowingsRepository.Setup(r => r.GetFollowing(_UserId, _UserId + "-")).Returns(following);

            // Act
            var result = _controller.Follow(followingDto).Result;

            // Assert
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void Follow_AttemptingToFollowYourself_ShouldReturnBadRequest()
        {
            // Arrange
            var followingDto = new FollowingDto { FolloweeId = _UserId };
            var following = new Following();
            _mockFollowingsRepository.Setup(r => r.GetFollowing(_UserId, _UserId)).Returns(following);

            // Act
            var result = _controller.Follow(followingDto).Result;

            // Assert
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void Follow_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var followingDto = new FollowingDto { FolloweeId = _UserId + "-" };
            _mockFollowingsRepository.Setup(r => r.GetFollowing(_UserId, _UserId + "-")).Returns((Following)null);

            // Act
            var result = _controller.Follow(followingDto).Result;

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Unfollow_AttemptingToUnfollowNotExistentFollowing_ShouldReturnBadRequest()
        {
            // Arrange
            _mockFollowingsRepository.Setup(r => r.GetFollowing(_UserId, _UserId + "-")).Returns((Following)null);

            // Act
            var result = _controller.Unfollow(_UserId + "-").Result;

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Unfollow_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var following = new Following();
            _mockFollowingsRepository.Setup(r => r.GetFollowing(_UserId, _UserId + "-")).Returns(following);

            // Act
            var result = _controller.Unfollow(_UserId + "-").Result;

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
