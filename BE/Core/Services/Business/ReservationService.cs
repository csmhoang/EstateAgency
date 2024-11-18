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
    internal sealed class ReservationService : ServiceBase<Reservation>, IReservationService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ReservationService(IRepositoryManager repository,
                   ILoggerManager logger,
                   IMapper mapper) : base(repository.Reservation)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var reservations = await _repository.Reservation.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<ReservationDto>>(reservations),
                StatusCode = !reservations.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var reservation = await _repository.Reservation.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<ReservationDto>(reservation),
                StatusCode = reservation is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> DeleteAsync(string id)
        {
            var reservationDelete = await _repository.Reservation.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (reservationDelete is not null)
            {
                _repository.Reservation.Delete(reservationDelete);
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
                throw new ReservationNotFoundException(id);
            }
        }
        public async Task<Response> InsertAsync(ReservationDto reservationDto)
        {
            await ValidateObject(reservationDto);

            var reservation = _mapper.Map<Reservation>(reservationDto);
            _repository.Reservation.Create(reservation);
            await _repository.SaveAsync();

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> UpdateAsync(string id, ReservationDto reservationDto)
        {
            await ValidateObject(reservationDto);

            var reservation = await _repository.Reservation.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (reservation is not null)
            {
                _mapper.Map(reservationDto, reservation);
                _repository.Reservation.Update(reservation);
                await _repository.SaveAsync();
            }
            else
            {
                throw new ReservationNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public async Task ValidateObject(ReservationDto reservationDto)
        {
            var room = await _repository.Room.FindCondition(r => r.Id.Equals(reservationDto.PostId)).FirstOrDefaultAsync();
            if (room is not null)
            {
                if (reservationDto.TenantId!.Equals(room.LandlordId))
                {
                    throw new CustomizeException(Invalidate.TenantIdAndLandlordIdDuplication);
                }
            }
            else
            {
                throw new PostNotFoundException(reservationDto.PostId);
            }
        }
        #endregion
    }
}