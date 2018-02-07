using GigHub.Core.Models;
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

        [SetUp]
        public void Init()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockContext = new Mock<IApplicationDbContext>();
        }

        [TearDown]
        public void Dispose()
        {
            _mockContext = null;
            _mockGigs = null;
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            // Arrange
            var userId = "1";
            var gig = new Gig() {
                DateTime = DateTime.Now.AddDays(-1),
                ArtistId = userId
            };
            /* Note the repository must be initialized after mockGigs due to 
             * a Moq feature that was removed. In particular, _mockContext
             * depends on _mockGigs and repository depends on _mockContext */
            _mockGigs.SetSource(new List<Gig> { gig });
            _mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            var repository = new GigRepository(_mockContext.Object);

            // Act
            var gigs = repository.GetUpcomingGigsByArtist(userId);

            // Assert
            Assert.IsEmpty(gigs);
        }
    }
}
