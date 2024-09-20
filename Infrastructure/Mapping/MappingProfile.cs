using AutoMapper;
using Core.Dtos;
using Core.Entities;

namespace Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterDto, User>();
            CreateMap<RoomDto, Room>().ReverseMap();
        }
    }
}
