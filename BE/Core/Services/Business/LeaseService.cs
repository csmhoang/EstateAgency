using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Resources;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Core.Enums.BookingEnums;
using static Core.Enums.InvoiceEnums;
using static Core.Enums.LeaseEnums;

namespace Core.Services.Business
{
    internal class LeaseService : ServiceBase<Lease>, ILeaseService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public LeaseService(IRepositoryManager repository,
                   ILoggerManager logger,
                   IMapper mapper) : base(repository.Lease)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var leases = await _repository.Lease.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<LeaseDto>>(leases),
                StatusCode = !leases.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var lease = await _repository.Lease.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<LeaseDto>(lease),
                StatusCode = lease is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetByRoomIdAsync(string roomId)
        {
            var lease = await _repository.LeaseDetail
                .FindCondition(ld => ld.RoomId == roomId && ld.Lease!.Status == StatusLease.Active)
                .Include(ld => ld.Lease!)
                .ThenInclude(l => l.Booking!)
                .Include(ld => ld.Lease!)
                .ThenInclude(l => l.LeaseDetails!)
                .ThenInclude(l => l.Room!)
                .Select(ld => ld.Lease!.Booking)
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<LeaseDto>(lease),
                StatusCode = lease is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> ResponseAsync(string id, StatusLease status)
        {
            var lease = await _repository.Lease.FindCondition(r => r.Id.Equals(id))
                .Include(l => l.Booking!)
                .ThenInclude(l => l.Invoice!)
                .FirstOrDefaultAsync();
            if (lease == null) throw new LeaseNotFoundException(id);
            lease.Status = status;
            lease.UpdatedAt = DateTime.Now;
            _repository.Lease.Update(lease);
            if (status == StatusLease.Canceled)
            {
                var booking = lease.Booking!;
                booking.Status = StatusBooking.Canceled;
                booking.UpdatedAt = DateTime.Now;
                _repository.Booking.Update(booking);
                var invoice = lease.Booking!.Invoice!;
                invoice.Status = StatusInvoice.Cancelled;
                invoice.UpdatedAt = DateTime.Now;
                _repository.Invoice.Update(invoice);

            }
            await _repository.SaveAsync();
            return new Response
            {
                Success = true,
                Messages = Successfull.ResponseSucceed,
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }

        public async Task<Response> DeleteAsync(string id)
        {
            var leaseDelete = await _repository.Lease.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (leaseDelete is not null)
            {
                _repository.Lease.Delete(leaseDelete);
                await _repository.SaveAsync();
                return new Response
                {
                    Success = true,
                    Messages = Successfull.DeleteSucceed,
                    StatusCode = (int)HttpStatusCode.NoContent
                };
            }
            else
            {
                throw new LeaseNotFoundException(id);
            }
        }
        public async Task<Response> InsertAsync(LeaseDto leaseDto)
        {
            var booking = await _repository.Booking.FindCondition((b) => b.Id.Equals(leaseDto.BookingId))
                .Include(b => b.BookingDetails!)
                .ThenInclude(bd => bd.Room)
                .FirstOrDefaultAsync();
            if (booking == null) throw new BookingNotFoundException(leaseDto.BookingId);
            var lease = _mapper.Map<Lease>(leaseDto);
            var invoice = new Invoice
            {
                DueDate = DateTime.Now.AddHours(12),
            };
            booking.InvoiceId = invoice.Id;
            decimal amount = 0;
            foreach (var bookingDetail in booking.BookingDetails)
            {
                if (bookingDetail.Status == StatusBookingDetail.Accepted)
                {
                    lease.LeaseDetails.Add(new LeaseDetail
                    {
                        RoomId = bookingDetail.RoomId,
                        LeaseId = lease.Id,
                        StartDate = bookingDetail.StartDate,
                        EndDate = bookingDetail.StartDate.AddMonths(bookingDetail.NumberOfMonth),
                        NumberOfTenant = bookingDetail.NumberOfTenant,
                        Price = bookingDetail.Price
                    });

                    invoice.InvoiceDetails.Add(new InvoiceDetail
                    {
                        Detail = bookingDetail.Room!.Name,
                        Price = bookingDetail.Price
                    });
                    amount += bookingDetail.Price;
                }
            }
            if (lease.LeaseDetails.Count == 0)
                throw new CustomizeException(Invalidate.BookingEmpty, (int)HttpStatusCode.NotModified);
            invoice.Amount = amount;
            _repository.Lease.Create(lease);
            _repository.Invoice.Create(invoice);
            _repository.Booking.Update(booking);
            await _repository.SaveAsync();

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> UpdateAsync(string id, LeaseDto leaseDto)
        {
            var lease = await _repository.Lease.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (lease is not null)
            {
                _mapper.Map(leaseDto, lease);
                _repository.Lease.Update(lease);
                await _repository.SaveAsync();
            }
            else
            {
                throw new LeaseNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        #endregion
    }
}
