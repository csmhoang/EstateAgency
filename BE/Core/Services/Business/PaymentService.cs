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
        public async Task<Response> InsertAsync(PaymentDto paymentDto)
        {
            await ValidateObject(paymentDto);

            var payment = _mapper.Map<Payment>(paymentDto);
            _repository.Payment.Create(payment);
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
