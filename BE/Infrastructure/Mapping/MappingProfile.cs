using AutoMapper;
using Core.Dtos;
using Core.Entities;
using System.ComponentModel;

namespace Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, UserDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<RegisterDto, User>();

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>()
               .ForAllMembers(opts =>
               {
                   opts.AllowNull();
                   opts.Condition((src, dest, srcMember) => srcMember != null);
               });

            CreateMap<Lease, LeaseDto>();
            CreateMap<LeaseDto, Lease>()
               .ForAllMembers(opts =>
               {
                   opts.AllowNull();
                   opts.Condition((src, dest, srcMember) => srcMember != null);
               });

            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>()
               .ForAllMembers(opts =>
               {
                   opts.AllowNull();
                   opts.Condition((src, dest, srcMember) => srcMember != null);
               });
        }
    }
}
