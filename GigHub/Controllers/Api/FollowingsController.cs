using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (userId == dto.FolloweeId)
                return BadRequest("User cannot follow itself.");

            var existingFollowing = _unitOfWork.Followings.GetFollowing(userId, dto.FolloweeId);
            if (existingFollowing != null)
                return BadRequest("Following already exists.");

            var following = new Following
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };

            _unitOfWork.Followings.Add(following);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _unitOfWork.Followings.GetFollowing(userId, id);

            if (following == null)
                return NotFound();
            
            _unitOfWork.Followings.Remove(following);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
