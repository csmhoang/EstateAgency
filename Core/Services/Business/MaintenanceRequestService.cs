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
    internal class MaintenanceRequestService : ServiceBase<MaintenanceRequest>, IMaintenanceRequestService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public MaintenanceRequestService(IRepositoryManager repository,
                   ILoggerManager logger,
                   IMapper mapper) : base(repository.MaintenanceRequest)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var maintenanceRequestRepositorys = await _repository.MaintenanceRequest.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<MaintenanceRequestDto>>(maintenanceRequestRepositorys),
                StatusCode = !maintenanceRequestRepositorys.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var maintenanceRequestRepository = await _repository.MaintenanceRequest.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<MaintenanceRequestDto>(maintenanceRequestRepository),
                StatusCode = maintenanceRequestRepository is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> DeleteAsync(string id)
        {
            var maintenanceRequestRepositoryDelete = await _repository.MaintenanceRequest.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (maintenanceRequestRepositoryDelete is not null)
            {
                _repository.MaintenanceRequest.Delete(maintenanceRequestRepositoryDelete);
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
                throw new MaintenanceRequestNotFoundException(id);
            }
        }
        public async Task<Response> InsertAsync(MaintenanceRequestDto maintenanceRequestRepositoryDto)
        {
            await ValidateObject(maintenanceRequestRepositoryDto);

            var maintenanceRequestRepository = _mapper.Map<MaintenanceRequest>(maintenanceRequestRepositoryDto);
            _repository.MaintenanceRequest.Create(maintenanceRequestRepository);
            await _repository.SaveAsync();

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> UpdateAsync(string id, MaintenanceRequestDto maintenanceRequestRepositoryDto)
        {
            await ValidateObject(maintenanceRequestRepositoryDto);

            var maintenanceRequestRepository = await _repository.MaintenanceRequest.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (maintenanceRequestRepository is not null)
            {
                _mapper.Map(maintenanceRequestRepositoryDto, maintenanceRequestRepository);
                _repository.MaintenanceRequest.Update(maintenanceRequestRepository);
                await _repository.SaveAsync();
            }
            else
            {
                throw new MaintenanceRequestNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public Task ValidateObject(MaintenanceRequestDto maintenanceRequestRepositoryDto)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
