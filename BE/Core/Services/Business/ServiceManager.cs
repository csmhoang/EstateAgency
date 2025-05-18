using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Services.Business;

public class ServiceManager : IServiceManager
{
    #region Declaration
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<Interfaces.Auth.IAuthenticationService> _authenticationService;
    private readonly Lazy<IRoomService> _roomService;
    private readonly Lazy<IPostService> _postService;
    private readonly Lazy<IReservationService> _reservationService;
    private readonly Lazy<IBookingService> _bookingService;
    private readonly Lazy<IBookingDetailService> _bookingDetailService;
    private readonly Lazy<ILeaseService> _leaseService;
    private readonly Lazy<IInvoiceService> _invoiceService;
    private readonly Lazy<IPaymentService> _paymentService;
    private readonly Lazy<IAmenityService> _amenityService;
    private readonly Lazy<IPhotoService> _photoService;
    private readonly Lazy<ICartService> _cartService;
    private readonly Lazy<INotificationService> _notificationService;
    private readonly Lazy<IDashboardService> _dashboardService;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public ServiceManager(
        IRepositoryManager repository,
        IPhotoService photoService,
        IEmailSender emailSender,
        ILoggerManager logger,
        IMapper mapper,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IConfiguration configuration,
        IOptions<CloudinarySettings> cloudinaryConfig)
    {
        _userService = new Lazy<IUserService>(() =>
            new UserService(repository, photoService, logger, mapper));
        _authenticationService = new Lazy<Interfaces.Auth.IAuthenticationService>(() =>
            new AuthenticationService(repository, logger, mapper, emailSender, userManager, roleManager, configuration));
        _roomService = new Lazy<IRoomService>(() =>
            new RoomService(repository, photoService, logger, mapper));
        _postService = new Lazy<IPostService>(() =>
            new PostService(repository, logger, mapper));
        _reservationService = new Lazy<IReservationService>(() =>
            new ReservationService(repository, logger, mapper));
        _bookingService = new Lazy<IBookingService>(() =>
            new BookingService(repository, logger, mapper));
        _bookingDetailService = new Lazy<IBookingDetailService>(() =>
            new BookingDetailService(repository, logger, mapper));
        _leaseService = new Lazy<ILeaseService>(() =>
            new LeaseService(repository, logger, mapper));
        _invoiceService = new Lazy<IInvoiceService>(() =>
            new InvoiceService(repository, logger, mapper));
        _paymentService = new Lazy<IPaymentService>(() =>
            new PaymentService(repository, logger, mapper));
        _amenityService = new Lazy<IAmenityService>(() =>
            new AmenityService(repository, logger, mapper));
        _photoService = new Lazy<IPhotoService>(() =>
            new PhotoService(cloudinaryConfig));
        _cartService = new Lazy<ICartService>(() =>
            new CartService(repository, logger, mapper));
        _notificationService = new Lazy<INotificationService>(() =>
            new NotificationService(repository, logger, mapper));
        _dashboardService = new Lazy<IDashboardService>(() =>
            new DashboardService(repository, logger, mapper));
    }
    #endregion

    #region Method
    public IUserService User => _userService.Value;
    public Interfaces.Auth.IAuthenticationService Authentication
        => _authenticationService.Value;
    public IRoomService Room => _roomService.Value;
    public IReservationService Reservation => _reservationService.Value;
    public ILeaseService Lease => _leaseService.Value;
    public IInvoiceService Invoice => _invoiceService.Value;
    public IPaymentService Payment => _paymentService.Value;
    public IAmenityService Amenity => _amenityService.Value;
    public IPhotoService Photo => _photoService.Value;
    public IPostService Post => _postService.Value;
    public IBookingService Booking => _bookingService.Value;
    public ICartService Cart => _cartService.Value;
    public IBookingDetailService BookingDetail => _bookingDetailService.Value;
    public INotificationService Notification => _notificationService.Value;
    public IDashboardService Dashboard => _dashboardService.Value;
    #endregion
}
