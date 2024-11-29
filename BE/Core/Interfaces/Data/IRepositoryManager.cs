using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Data
{
    public interface IRepositoryManager
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
        IMaintenanceRequestRepository MaintenanceRequest { get; }
        IAmenityRepository Amenity { get; }
        IPhotoRepository Photo { get; }
        IBookingRepository Booking { get; }
        ISavePostRepository SavePost { get; }
        IFollowRepository Follow { get; }
        IBookingDetailRepository BookingDetail { get; }
        ILeaseDetailRepository LeaseDetail { get; }
        ICartDetailRepository CartDetail { get; }
        ICartRepository Cart { get; }

        Task SaveAsync();
    }
}
