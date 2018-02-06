using GigHub.Models;
using GigHub.Persistence;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followings = _unitOfWork.Followings.GetFollowingForUser(userId);

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