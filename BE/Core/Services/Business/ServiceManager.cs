using AutoMapper;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Services.Auth;
using Core.Services.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Services.Business
{
    public class ServiceManager : IServiceManager
    {
        #region Declaration
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<Interfaces.Auth.IAuthenticationService> _authenticationService;
        private readonly Lazy<IRoomService> _roomService;
        private readonly Lazy<IPostService> _postService;
        private readonly Lazy<IReservationService> _reservationService;
        private readonly Lazy<IBookingService> _bookingService;
        private readonly Lazy<ILeaseService> _leaseService;
        private readonly Lazy<IInvoiceService> _invoiceService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<IMaintenanceRequestService> _maintenanceRequestService;
        private readonly Lazy<IAmenityService> _amenityService;
        private readonly Lazy<IPhotoService> _photoService;
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
                new AuthenticationService(logger, mapper, emailSender, userManager, roleManager, configuration));
            _roomService = new Lazy<IRoomService>(() =>
                new RoomService(repository, photoService, logger, mapper));
            _postService = new Lazy<IPostService>(() =>
                new PostService(repository, logger, mapper));
            _reservationService = new Lazy<IReservationService>(() =>
                new ReservationService(repository, logger, mapper));
            _bookingService = new Lazy<IBookingService>(() =>
                new BookingService(repository, logger, mapper));
            _leaseService = new Lazy<ILeaseService>(() =>
                new LeaseService(repository, logger, mapper));
            _invoiceService = new Lazy<IInvoiceService>(() =>
                new InvoiceService(repository, logger, mapper));
            _paymentService = new Lazy<IPaymentService>(() =>
                new PaymentService(repository, logger, mapper));
            _maintenanceRequestService = new Lazy<IMaintenanceRequestService>(() =>
                new MaintenanceRequestService(repository, logger, mapper));
            _amenityService = new Lazy<IAmenityService>(() =>
                new AmenityService(repository, logger, mapper));
            _photoService = new Lazy<IPhotoService>(() =>
                new PhotoService(cloudinaryConfig));
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

        public IMaintenanceRequestService MaintenanceRequest
            => _maintenanceRequestService.Value;

        public IAmenityService Amenity => _amenityService.Value;

        public IPhotoService Photo => _photoService.Value;

        public IPostService Post => _postService.Value;

        public IBookingService Booking => _bookingService.Value;
        #endregion
    }
}
