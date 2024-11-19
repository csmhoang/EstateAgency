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
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.NumberOfFollowers, opt => opt.MapFrom(src => src.Followers.Count))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<Follow, UserDto>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserRole, UserRoleDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Role, RoleDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<RegisterDto, User>();

            CreateMap<UserUpdateDto, User>();

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<RoomUpdateDto, Room>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto, Photo>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<SavePost, SavePostDto>();
            CreateMap<SavePostDto, SavePost>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<PostUpdateDto, Post>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Booking, BookingDto>();
            CreateMap<BookingDto, Booking>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Lease, LeaseDto>();
            CreateMap<LeaseDto, Lease>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Feedback, FeedbackDto>();
            CreateMap<FeedbackDto, Feedback>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
