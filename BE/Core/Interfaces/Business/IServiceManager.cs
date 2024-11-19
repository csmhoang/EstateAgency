using Core.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
