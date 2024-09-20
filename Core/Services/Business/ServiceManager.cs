using AutoMapper;
using Core.Entities;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Core.Services.Business
{
    public class ServiceManager : IServiceManager
    {
        #region Declaration
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<Interfaces.Auth.IAuthenticationService> _authenticationService;
        private readonly Lazy<IRoomService> _roomService;
        private readonly Lazy<IReservationService> _reservationService;
        private readonly Lazy<ILeaseService> _leaseService;
        private readonly Lazy<IInvoiceService> _invoiceService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<IMaintenanceRequestService> _maintenanceRequestService;
        private readonly Lazy<IAmenityService> _amenityService;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ServiceManager(
            IRepositoryManager repository,
            ILoggerManager logger,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userService = new Lazy<IUserService>(() =>
                new UserService(repository, logger, mapper));
            _authenticationService = new Lazy<Interfaces.Auth.IAuthenticationService>(() =>
                new AuthenticationService(logger, mapper, userManager, configuration));
            _roomService = new Lazy<IRoomService>(() =>
                new RoomService(repository, logger, mapper));
            _reservationService = new Lazy<IReservationService>(() =>
                new ReservationService(repository, logger, mapper));
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

        public IMaintenanceRequestService MaintenanceRequest => _maintenanceRequestService.Value;

        public IAmenityService Amenity => _amenityService.Value;
        #endregion
    }
}
