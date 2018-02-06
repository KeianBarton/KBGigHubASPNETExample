using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Models.Notifications;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}