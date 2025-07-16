using AutoMapper;
using CommunityEventHub.Models;
using CommunityEventHub.Models.Dto;

namespace CommunityEventHub.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEventDto, Meetup>();
            CreateMap<CreateEventDto, Webinar>();
            CreateMap<CreateEventDto, Conference>();
            CreateMap<CreateEventDto, JobFair>();
            CreateMap<EventBase, EventDto>();
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<EventBase, EventDto>();
            CreateMap<CreateEventDto, EventBase>()
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());
        }
    }
}
