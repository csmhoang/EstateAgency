namespace Core;

public interface IServiceManager
{
    IUserService User { get; }
    IAuthenticationService Authentication { get; }
    IRoomService Room { get; }
    IPostService Post { get; }
    IReservationService Reservation { get; }
    ILeaseService Lease { get; }
    IBookingService Booking { get; }
    IBookingDetailService BookingDetail { get; }
    IInvoiceService Invoice { get; }
    IPaymentService Payment { get; }
    IAmenityService Amenity { get; }
    IPhotoService Photo { get; }
    ICartService Cart { get; }
    INotificationService Notification { get; }
    IDashboardService Dashboard { get; }
}
