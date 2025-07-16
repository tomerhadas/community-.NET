using AutoMapper;
using CommunityEventHub.Models;
using CommunityEventHub.Models.Dto;

namespace CommunityEventHub.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<EventBase, EventDto>();
            CreateMap<CreateEventDto, EventBase>()
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());
        }
    }
}
