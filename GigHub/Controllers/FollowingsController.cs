using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followings = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Include(f => f.Follower)
                .Include(f => f.Followee)
                .ToList();

            var viewModel = new FollowingsViewModel
            {
                Followings = followings,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Artists I'm Following"
            };

            return View("Followings", viewModel);
        }
    }
}