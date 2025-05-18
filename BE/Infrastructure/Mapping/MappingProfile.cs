using AutoMapper;

namespace Infrastructure;

public class MappingProfile : Profile
{
    //private List<string> CastToListString(object src)
    //{
    //    var values = new List<string>();
    //    var type = src.GetType();
    //    var props = type.GetProperties();
    //    foreach (var prop in props)
    //    {
    //        if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(int))
    //        {
    //            var value = prop.GetValue(src)?.ToString();
    //            if (!string.IsNullOrEmpty(value))
    //            {
    //                values.Add(value.ToString() ?? "");
    //            }
    //        }
    //    }
    //    return values;
    //}
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.NumberOfFollowers, opt => opt.MapFrom(src => src.Followers.Count));

        //CreateMap<User, OptionDto>()
        //    .MaxDepth(1)
        //    .AfterMap((src, dest) =>
        //    {
        //        dest.values = CastToListString(src);
        //    })
        //    .ForAllMembers(opt => opt.Ignore());

        CreateMap<Follow, FollowDto>().MaxDepth(1);

        CreateMap<UserRole, UserRoleDto>();

        CreateMap<Role, RoleDto>();

        CreateMap<Room, RoomDto>();

        CreateMap<Photo, PhotoDto>();

        CreateMap<Post, PostDto>();

        CreateMap<SavePost, SavePostDto>();

        CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.ReservationHour, opt => opt.MapFrom(src => src.ReservationDate.Hour))
            .ForMember(dest => dest.ReservationMinute, opt => opt.MapFrom(src => src.ReservationDate.Minute));

        CreateMap<Booking, BookingDto>();

        CreateMap<BookingDetail, BookingDetailDto>();

        CreateMap<Lease, LeaseDto>();

        CreateMap<LeaseDetail, LeaseDetailDto>();

        CreateMap<Cart, CartDto>();

        CreateMap<CartDetail, CartDetailDto>();

        CreateMap<Invoice, InvoiceDto>();

        CreateMap<Payment, PaymentDto>();

        CreateMap<InvoiceDetail, InvoiceDetailDto>();

        CreateMap<Feedback, FeedbackDto>();

        CreateMap<Message, MessageDto>();

        CreateMap<Conversation, ConversationDto>();

        CreateMap<Notification, NotificationDto>();

        CreateMap<Participant, ParticipantDto>();

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<RegisterDto, User>();

        CreateMap<UserUpdateDto, User>();

        CreateMap<RoomDto, Room>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<RoomUpdateDto, Room>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<PhotoDto, Photo>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<PostDto, Post>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<SavePostDto, SavePost>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<PostUpdateDto, Post>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ReservationDto, Reservation>()
            .ForMember(dest => dest.ReservationDate, opt => opt
                .MapFrom(src => src.ReservationDate.Date
                    .AddHours(src.ReservationHour)
                    .AddMinutes(src.ReservationMinute)
                )
            )
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ReservationUpdateDto, Reservation>()
            .ForMember(dest => dest.ReservationDate, opt => opt
                .MapFrom(src => src.ReservationDate.Date
                    .AddHours(src.ReservationHour)
                    .AddMinutes(src.ReservationMinute)
                )
            );

        CreateMap<BookingDto, Booking>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<LeaseDto, Lease>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<FeedbackDto, Feedback>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<MessageDto, Message>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ConversationDto, Conversation>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<NotificationDto, Notification>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ParticipantDto, Participant>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<InvoiceDto, Invoice>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<PaymentDto, Payment>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<InvoiceDetailDto, InvoiceDetail>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CartDto, Cart>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CartDetailDto, CartDetail>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
