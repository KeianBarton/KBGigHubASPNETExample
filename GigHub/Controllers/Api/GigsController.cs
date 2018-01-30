using GigHub.Helpers.Notifications;
using GigHub.Models;
using GigHub.Models.Notifications;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationHelper _notificationHelper;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _notificationHelper = new NotificationHelper();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancelled)
                return NotFound(); // act as if the record has been deleted

            var attendees = _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();

            _notificationHelper.AddUserNotifications(
                gig, attendees, NotificationType.GigCancelled);

            gig.IsCancelled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
