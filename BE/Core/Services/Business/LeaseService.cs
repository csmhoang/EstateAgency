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
            var lease = _mapper.Map<Lease>(leaseDto);
            _repository.Lease.Create(lease);
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
