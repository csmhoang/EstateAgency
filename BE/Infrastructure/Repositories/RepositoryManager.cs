using Core.Entities;
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
        private readonly Lazy<IFeedbackRepository> _feedbackRepository;
        private readonly Lazy<IReservationRepository> _reservationRepository;
        private readonly Lazy<ILeaseRepository> _leaseRepository;
        private readonly Lazy<IInvoiceRepository> _invoiceRepository;
        private readonly Lazy<IInvoiceDetailRepository> _invoiceDetailRepository;
        private readonly Lazy<IPaymentRepository> _paymentRepository;
        private readonly Lazy<IBookingRepository> _bookingRepository;
        private readonly Lazy<ISavePostRepository> _savePostRepository;
        private readonly Lazy<IAmenityRepository> _amenityRepository;
        private readonly Lazy<IPhotoRepository> _photoRepository;
        private readonly Lazy<IFollowRepository> _followRepository;
        private readonly Lazy<ILeaseDetailRepository> _leaseDetailRepository;
        private readonly Lazy<IBookingDetailRepository> _bookingDetailRepository;
        private readonly Lazy<ICartDetailRepository> _cartDetailRepository;
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<IMessageRepository> _messageRepository;
        private readonly Lazy<IConversationRepository> _conversationRepository;
        private readonly Lazy<IParticipantRepository> _participantRepository;
        private readonly Lazy<INotificationRepository> _notificationRepository;
        private readonly Lazy<IVisitStatRepository> _visitStatRepository;
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
            _feedbackRepository = new Lazy<IFeedbackRepository>(() => new FeedbackRepository(context));
            _reservationRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(context));
            _leaseRepository = new Lazy<ILeaseRepository>(() => new LeaseRepository(context));
            _invoiceRepository = new Lazy<IInvoiceRepository>(() => new InvoiceRepository(context));
            _invoiceDetailRepository = new Lazy<IInvoiceDetailRepository>(() => new InvoiceDetailRepository(context));
            _paymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(context));
            _bookingRepository = new Lazy<IBookingRepository>(() => new BookingRepository(context));
            _savePostRepository = new Lazy<ISavePostRepository>(() => new SavePostRepository(context));
            _amenityRepository = new Lazy<IAmenityRepository>(() => new AmenityRepository(context));
            _photoRepository = new Lazy<IPhotoRepository>(() => new PhotoRepository(context));
            _followRepository = new Lazy<IFollowRepository>(() => new FollowRepository(context));
            _leaseDetailRepository = new Lazy<ILeaseDetailRepository>(() => new LeaseDetailRepository(context));
            _bookingDetailRepository = new Lazy<IBookingDetailRepository>(() => new BookingDetailRepository(context));
            _cartDetailRepository = new Lazy<ICartDetailRepository>(() => new CartDetailRepository(context));
            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(context));
            _messageRepository = new Lazy<IMessageRepository>(() => new MessageRepository(context));
            _conversationRepository = new Lazy<IConversationRepository>(() => new ConversationRepository(context));
            _participantRepository = new Lazy<IParticipantRepository>(() => new ParticipantRepository(context));
            _notificationRepository = new Lazy<INotificationRepository>(() => new NotificationRepository(context));
            _visitStatRepository = new Lazy<IVisitStatRepository>(() => new VisitStatRepository(context));
        }
        #endregion

        #region Method
        public IUserRepository User => _userRepository.Value;
        public IRoomRepository Room => _roomRepository.Value;
        public IReservationRepository Reservation => _reservationRepository.Value;
        public ILeaseRepository Lease => _leaseRepository.Value;
        public IInvoiceRepository Invoice => _invoiceRepository.Value;
        public IPaymentRepository Payment => _paymentRepository.Value;
        public IAmenityRepository Amenity => _amenityRepository.Value;
        public IPhotoRepository Photo => _photoRepository.Value;
        public IPostRepository Post => _postRepository.Value;
        public IFeedbackRepository Feedback => _feedbackRepository.Value;
        public IBookingRepository Booking => _bookingRepository.Value;
        public ISavePostRepository SavePost => _savePostRepository.Value;
        public IFollowRepository Follow => _followRepository.Value;
        public IInvoiceDetailRepository InvoiceDetail => _invoiceDetailRepository.Value;
        public IBookingDetailRepository BookingDetail => _bookingDetailRepository.Value;
        public ILeaseDetailRepository LeaseDetail => _leaseDetailRepository.Value;
        public ICartDetailRepository CartDetail => _cartDetailRepository.Value;
        public ICartRepository Cart => _cartRepository.Value;
        public IMessageRepository Message => _messageRepository.Value;
        public IConversationRepository Conversation => _conversationRepository.Value;
        public IParticipantRepository Participant => _participantRepository.Value;
        public INotificationRepository Notification => _notificationRepository.Value;
        public IVisitStatRepository VisitStat => _visitStatRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
        #endregion
    }
}
