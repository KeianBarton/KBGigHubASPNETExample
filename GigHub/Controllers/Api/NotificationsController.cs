using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models.Notifications;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notifications.GetUnreadUserNotificationsWithArtist(userId);

            return notifications.Select(
                Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public async Task<IHttpActionResult> MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var userNotifications = _unitOfWork.Notifications.GetUnreadUserNotifications(userId);

            userNotifications.ForEach(u => u.MarkAsRead());

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
