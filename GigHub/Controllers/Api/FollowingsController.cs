using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GigHub.Controllers.Api
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
        public async Task<IHttpActionResult> Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var followingAlreadyExists = _context.Followings
                .Any(f => f.FollowerId == userId
                        && f.FolloweeId == dto.FolloweeId);
            var followingYourself = (userId == dto.FolloweeId);

            if (followingAlreadyExists)
                return BadRequest("Following already exists.");
            if (followingYourself)
                return BadRequest("User cannot follow itself.");

            var following = new Following
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };

            _context.Followings.Add(following);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null)
                return NotFound();
            
            _context.Followings.Remove(following);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
