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
using static Core.Enums.RoomEnums;

namespace Core.Services.Business
{
    internal class PaymentService : ServiceBase<Payment>, IPaymentService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public PaymentService(IRepositoryManager repository,
                   ILoggerManager logger,
                   IMapper mapper) : base(repository.Payment)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var payments = await _repository.Payment.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<PaymentDto>>(payments),
                StatusCode = !payments.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var payment = await _repository.Payment.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<PaymentDto>(payment),
                StatusCode = payment is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> DeleteAsync(string id)
        {
            var paymentDelete = await _repository.Payment.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (paymentDelete is not null)
            {
                _repository.Payment.Delete(paymentDelete);
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
                throw new PaymentNotFoundException(id);
            }
        }
        public async Task<Response> InsertAsync(string invoiceId)
        {
            var invoice = await _repository.Invoice.FindCondition(r => r.Id.Equals(invoiceId))
                .Include(i => i.Booking!)
                .ThenInclude(b => b.BookingDetails!)
                .ThenInclude(bd => bd.Room!)
                .FirstOrDefaultAsync();
            if (invoice == null) throw new InvoiceNotFoundException(invoiceId);

            _repository.Payment.Create(new Payment
            {
                InvoiceId = invoice.Id,
                Amount = invoice.Amount,
                PaymentDate = DateTime.UtcNow
            });
            foreach (var bookingDetail in invoice.Booking!.BookingDetails)
            {
                if (bookingDetail.Status == StatusBookingDetail.Accepted)
                {
                    var room = bookingDetail.Room!;
                    room.Condition = ConditionRoom.Occupied;
                    _repository.Room.Update(room);
                }
            }
            invoice.Status = StatusInvoice.Paid;
            _repository.Invoice.Update(invoice);
            await _repository.SaveAsync();

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> UpdateAsync(string id, PaymentDto paymentDto)
        {
            await ValidateObject(paymentDto);

            var payment = await _repository.Payment.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (payment is not null)
            {
                _mapper.Map(paymentDto, payment);
                _repository.Payment.Update(payment);
                await _repository.SaveAsync();
            }
            else
            {
                throw new PaymentNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public Task ValidateObject(PaymentDto paymentDto)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
