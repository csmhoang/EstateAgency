using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Business
{
    internal class InvoiceService : ServiceBase<Invoice>, IInvoiceService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public InvoiceService(IRepositoryManager repository,
                   ILoggerManager logger,
                   IMapper mapper) : base(repository.Invoice)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var invoices = await _repository.Invoice.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<InvoiceDto>>(invoices),
                StatusCode = !invoices.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var invoice = await _repository.Invoice.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<InvoiceDto>(invoice),
                StatusCode = invoice is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> DeleteAsync(string id)
        {
            var invoiceDelete = await _repository.Invoice.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (invoiceDelete is not null)
            {
                _repository.Invoice.Delete(invoiceDelete);
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
                throw new InvoiceNotFoundException(id);
            }
        }
        public async Task<Response> InsertAsync(InvoiceDto invoiceDto)
        {
            await ValidateObject(invoiceDto);

            var invoice = _mapper.Map<Invoice>(invoiceDto);
            _repository.Invoice.Create(invoice);
            await _repository.SaveAsync();

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> UpdateAsync(string id, InvoiceDto invoiceDto)
        {
            await ValidateObject(invoiceDto);

            var invoice = await _repository.Invoice.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (invoice is not null)
            {
                _mapper.Map(invoiceDto, invoice);
                _repository.Invoice.Update(invoice);
                await _repository.SaveAsync();
            }
            else
            {
                throw new InvoiceNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public Task ValidateObject(InvoiceDto invoiceDto)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
