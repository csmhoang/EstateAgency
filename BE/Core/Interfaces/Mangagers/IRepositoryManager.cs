namespace Core;

public interface IRepositoryManager : IDisposable
{
    IUserRepository User { get; }
    IRoomRepository Room { get; }
    IPostRepository Post { get; }
    IFeedbackRepository Feedback { get; }
    IReservationRepository Reservation { get; }
    ILeaseRepository Lease { get; }
    IInvoiceRepository Invoice { get; }
    IInvoiceDetailRepository InvoiceDetail { get; }
    IPaymentRepository Payment { get; }
    IAmenityRepository Amenity { get; }
    IPhotoRepository Photo { get; }
    IBookingRepository Booking { get; }
    ISavePostRepository SavePost { get; }
    IFollowRepository Follow { get; }
    IBookingDetailRepository BookingDetail { get; }
    ILeaseDetailRepository LeaseDetail { get; }
    ICartDetailRepository CartDetail { get; }
    ICartRepository Cart { get; }
    IMessageRepository Message { get; }
    IConversationRepository Conversation { get; }
    IParticipantRepository Participant { get; }
    INotificationRepository Notification { get; }
    IVisitStatRepository VisitStat { get; }

    Task SaveChangesAsync();
}
