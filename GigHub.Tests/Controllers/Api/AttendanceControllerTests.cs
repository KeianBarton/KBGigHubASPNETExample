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
    public class AttendanceControllerTests
    {
        private AttendancesController _controller;

        private Mock<IAttendanceRepository> _mockAttandanceRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        private const string _UserId = "1";
        private const string _UserName = "user1@domain.com";

        [SetUp]
        public void Init()
        {
            _mockAttandanceRepository = new Mock<IAttendanceRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.SetupGet(u => u.Attendances).Returns(_mockAttandanceRepository.Object);

            _controller = new AttendancesController(_mockUnitOfWork.Object);
            _controller.MockCurrentUser(_UserId, _UserName);
        }

        [TearDown]
        public void Dispose()
        {
            _controller = null;
            _mockAttandanceRepository = null;
            _mockUnitOfWork = null;
        }

        [Test]
        public void Attend_AttemptingToReAttend_ShouldReturnBadRequest()
        {
            // Arrange
            var attendanceDto = new AttendanceDto { GigId = 1 };
            var attendance = new Attendance();
            _mockAttandanceRepository.Setup(r => r.GetAttendance(_UserId, 1)).Returns(attendance);

            // Act
            var result = _controller.Attend(attendanceDto).Result;

            // Assert
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var attendanceDto = new AttendanceDto { GigId = 1 };
            _mockAttandanceRepository.Setup(r => r.GetAttendance(_UserId, 1)).Returns((Attendance)null);

            // Act
            var result = _controller.Attend(attendanceDto).Result;

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void DeleteAttendance_DeletingNonExistentAttendance_ShouldReturnBadRequest()
        {
            // Arrange
            _mockAttandanceRepository.Setup(r => r.GetAttendance(_UserId, 1)).Returns((Attendance)null);

            // Act
            var result = _controller.DeleteAttendance(1).Result;

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteAttendance_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            _mockAttandanceRepository.Setup(r => r.GetAttendance(_UserId, 1)).Returns(new Attendance());

            // Act
            var result = _controller.DeleteAttendance(1).Result;

            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<int>>(result);
        }

    }
}
