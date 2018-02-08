using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;

        private Mock<IGigRepository> _mockGigRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        private const string _UserId = "1";
        private const string _UserName = "user1@domain.com";

        [SetUp]
        public void SetUp()
        {
            _mockGigRepository = new Mock<IGigRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.SetupGet(u => u.Gigs).Returns(_mockGigRepository.Object);

            _controller = new GigsController(_mockUnitOfWork.Object);
            _controller.MockCurrentUser(_UserId, _UserName);
        }

        [TearDown]
        public void TearDown()
        {
            _controller = null;
            _mockGigRepository = null;
            _mockUnitOfWork = null;
        }

        [Test]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            // Act
            var result = _controller.Cancel(1).Result;

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Cancel_GigIsCancelled_ShouldReturnNotFound()
        {
            // Arrange
            var gig = new Gig();
            gig.Cancel();
            _mockGigRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            // Act
            var result = _controller.Cancel(1).Result;

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Cancel_UserCancellingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            // Arrange
            var gig = new Gig();
            _controller.MockCurrentUser(_UserId + "-", _UserName);
            _mockGigRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            // Act
            var result = _controller.Cancel(1).Result;

            // Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var gig = new Gig { ArtistId = _UserId };
            _mockGigRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            // Act
            var result = _controller.Cancel(1).Result;

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
