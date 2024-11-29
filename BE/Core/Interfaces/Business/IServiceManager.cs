using Core.Interfaces.Infrastructure;

namespace Core.Interfaces.Business
{
    public interface IServiceManager
    {
        IUserService User { get; }
        Auth.IAuthenticationService Authentication { get; }
        IRoomService Room { get; }
        IPostService Post { get; }
        IReservationService Reservation { get; }
        ILeaseService Lease { get; }
        IBookingService Booking { get; }
        IInvoiceService Invoice { get; }
        IPaymentService Payment { get; }
        IMaintenanceRequestService MaintenanceRequest { get; }
        IAmenityService Amenity { get; }
        IPhotoService Photo { get; }
        ICartService Cart { get; }
    }
}
