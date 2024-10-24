﻿using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        #region Declaration
        private readonly RepositoryContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IRoomRepository> _roomRepository;
        private readonly Lazy<IPostRepository> _postRepository;
        private readonly Lazy<IReservationRepository> _reservationRepository;
        private readonly Lazy<ILeaseRepository> _leaseRepository;
        private readonly Lazy<IInvoiceRepository> _invoiceRepository;
        private readonly Lazy<IPaymentRepository> _paymentRepository;
        private readonly Lazy<IMaintenanceRequestRepository> _maintenanceRequestRepository;
        private readonly Lazy<IAmenityRepository> _amenityRequestRepository;
        private readonly Lazy<IPhotoRepository> _photoRepository;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(context));
            _postRepository = new Lazy<IPostRepository>(() => new PostRepository(context));
            _reservationRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(context));
            _leaseRepository = new Lazy<ILeaseRepository>(() => new LeaseRepository(context));
            _invoiceRepository = new Lazy<IInvoiceRepository>(() => new InvoiceRepository(context));
            _paymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(context));
            _maintenanceRequestRepository = new Lazy<IMaintenanceRequestRepository>(() => new MaintenanceRequestRepository(context));
            _amenityRequestRepository = new Lazy<IAmenityRepository>(() => new AmenityRepository(context));
            _photoRepository = new Lazy<IPhotoRepository>(() => new PhotoRepository(context));
        }
        #endregion

        #region Method
        public IUserRepository User => _userRepository.Value;

        public IRoomRepository Room => _roomRepository.Value;

        public IReservationRepository Reservation => _reservationRepository.Value;

        public ILeaseRepository Lease => _leaseRepository.Value;

        public IInvoiceRepository Invoice => _invoiceRepository.Value;

        public IPaymentRepository Payment => _paymentRepository.Value;

        public IMaintenanceRequestRepository MaintenanceRequest => _maintenanceRequestRepository.Value;

        public IAmenityRepository Amenity => _amenityRequestRepository.Value;

        public IPhotoRepository Photo => _photoRepository.Value;

        public IPostRepository Post => _postRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
        #endregion
    }
}
