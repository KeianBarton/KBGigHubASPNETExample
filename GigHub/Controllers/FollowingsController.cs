using GigHub.Persistence;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FollowingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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