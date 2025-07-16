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
            // Add more mappings here...
        }
    }
}
