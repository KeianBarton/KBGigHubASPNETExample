using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var followingAlreadyExists = _context.Followings
                .Any(f => f.FollowerId == userId
                        && f.FolloweeId == dto.FolloweeId);
            if (followingAlreadyExists)
                return BadRequest("The attendance already exists.");

            var following = new Following
            {
                FolloweeId = userId,
                FollowerId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
