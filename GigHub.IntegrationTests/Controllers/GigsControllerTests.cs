using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistence;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _gigsController;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _gigsController = new GigsController(
                new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context = null;
            _gigsController = null;
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            // Arrange
            var user = _context.Users.First();
            _gigsController.MockCurrentUser(user.Id, user.UserName);
            var genre = _context.Genres.First();
            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = ""
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = _gigsController.Mine();

            // Assert
            Assert.That(result.ViewData.Model as IEnumerable<Gig>, Has.Count.EqualTo(1));
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenGig()
        {
            // Arrange
            var user = _context.Users.First();
            _gigsController.MockCurrentUser(user.Id, user.UserName);
            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = ""
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = _gigsController.Update(new GigFormViewModel
            {
                Id = gig.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2
            });

            // Assert
            _context.Entry(gig).Reload();
            Assert.AreEqual(
                DateTime.Today.AddMonths(1).AddHours(20), gig.DateTime);
            Assert.AreEqual("Venue", gig.Venue);
            Assert.AreEqual(2, gig.GenreId);
        }
    }
}
