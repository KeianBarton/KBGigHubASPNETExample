using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestFixture]
    public class GigRepositoryTests
    {
        private Mock<IApplicationDbContext> _mockContext;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendances;
        private IGigRepository _gigRepository;
        private IAttendanceRepository _attendanceRepository;

        [SetUp]
        public void SetUp()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();
            _mockContext = new Mock<IApplicationDbContext>();
        }

        private void InitializeAttendanceRepository(IList<Attendance> data)
        {
            /* Note the repository must be initialized after mockGigs due to 
             * a Moq feature that was removed. In particular, _mockContext
             * depends on _mockGigs and the repo depends on _mockContext */
            _mockAttendances.SetSource(data);
            _mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);
            _attendanceRepository = new AttendanceRepository(_mockContext.Object);
        }

        private void InitializeGigRepository(IList<Gig> data)
        {
            /* Note the repository must be initialized after mockGigs due to 
             * a Moq feature that was removed. In particular, _mockContext
             * depends on _mockGigs and the repo depends on _mockContext */
            _mockGigs.SetSource(data);
            _mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            _gigRepository = new GigRepository(_mockContext.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockContext = null;
            _mockGigs = null;
            _gigRepository = null;
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            // Arrange
            var userId = "1";
            var gig = new Gig()
            {
                DateTime = DateTime.Now.AddDays(-1),
                ArtistId = userId
            };
            InitializeGigRepository(new List<Gig> { gig });

            // Act
            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

            // Assert
            Assert.IsEmpty(gigs);
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigIsCancelled_ShouldNotBeReturned()
        {
            // Arrange
            var userId = "1";
            var gig = new Gig()
            {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = userId
            };
            gig.Cancel();
            InitializeGigRepository(new List<Gig> { gig });

            // Act
            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

            // Assert
            Assert.IsEmpty(gigs);
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            // Arrange
            var userId = "1";
            var gig = new Gig()
            {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = userId + "-"
            };
            InitializeGigRepository(new List<Gig> { gig });

            // Act
            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

            // Assert
            Assert.IsEmpty(gigs);
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            // Arrange
            var userId = "1";
            var gig = new Gig()
            {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = userId
            };
            InitializeGigRepository(new List<Gig> { gig });

            // Act
            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

            // Assert
            CollectionAssert.Contains(gigs, gig);
        }

        [Test]
        public void GetGigsUserAttendingIncludingCancelled_GigsInThePast_ShouldNotBeReturned()
        {
            // Arrange
            var userId = "1";
            var gig = new Gig()
            {
                DateTime = DateTime.Now.AddDays(-1),
                ArtistId = userId
            };
            var attendance = new Attendance()
            {
                AttendeeId = userId,
                Gig = gig
            };
            InitializeGigRepository(new List<Gig> { gig });
            InitializeAttendanceRepository(new List<Attendance> { attendance });

            // Act
            var gigs = _gigRepository.GetGigsUserAttendingIncludingCancelled(userId);

            // Assert
            Assert.IsEmpty(gigs);
        }

        [Test]
        public void GetGigsUserAttendingIncludingCancelled_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            // Arrange
            var userId = "1";
            var gig = new Gig()
            {
                DateTime = DateTime.Now.AddDays(-1),
                ArtistId = userId
            };
            var attendance = new Attendance()
            {
                AttendeeId = userId + "-",
                Gig = gig
            };
            InitializeGigRepository(new List<Gig> { gig });
            InitializeAttendanceRepository(new List<Attendance> { attendance });

            // Act
            var gigs = _gigRepository.GetGigsUserAttendingIncludingCancelled(userId);

            // Assert
            Assert.IsEmpty(gigs);
        }

        [Test]
        public void GetGigsUserAttendingIncludingCancelled_UpcomingGigUserAttending_ShouldBeReturned()
        {
            // Arrange
            var userId = "1";
            var gig = new Gig()
            {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = userId
            };
            var attendance = new Attendance()
            {
                AttendeeId = userId,
                Gig = gig
            };
            InitializeGigRepository(new List<Gig> { gig });
            InitializeAttendanceRepository(new List<Attendance> { attendance });

            // Act
            var gigs = _gigRepository.GetGigsUserAttendingIncludingCancelled(userId);

            // Assert
            CollectionAssert.Contains(gigs, gig);
        }
    }
}
