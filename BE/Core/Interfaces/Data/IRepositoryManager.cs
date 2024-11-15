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
        IPaymentRepository Payment { get; }
        IMaintenanceRequestRepository MaintenanceRequest { get; }
        IAmenityRepository Amenity { get; }
        IPhotoRepository Photo { get; }

        Task SaveAsync();
    }
}
