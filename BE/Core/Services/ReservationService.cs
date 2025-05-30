﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core;

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

    public async Task<Response> GetListAsync(ReservationSpecParams specParams)
    {
        var spec = new ReservationSpecification(specParams);
        var page = await CreatePagedResult(spec, specParams.PageIndex, specParams.PageSize);
        return new Response
        {
            Success = true,
            Data = new
            {
                page.PageIndex,
                page.PageSize,
                page.Count,
                Data = _mapper.Map<IEnumerable<ReservationDto>>(page.Data)
            },
            StatusCode = !page.Data.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> DeleteAsync(string id)
    {
        var reservationDelete = await _repository.Reservation.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if(reservationDelete != null) {
            if(reservationDelete.Status.Equals(ReservationEnums.StatusReservation.Confirmed)) {
                throw new CustomizeException(Invalidate.DeleteInvalidate, (int)HttpStatusCode.ResetContent);
            }

            _repository.Reservation.Delete(reservationDelete);
            await _repository.SaveChangesAsync();
            return new Response
            {
                Success = true,
                Messages = Successfull.DeleteSucceed,
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }
        else {
            throw new ReservationNotFoundException(id);
        }
    }
    public async Task<Response> InsertAsync(ReservationDto reservationDto)
    {
        var room = await _repository.Room.FindCondition(r =>
            r.Id.Equals(reservationDto.RoomId)
        ).FirstOrDefaultAsync();
        if(room == null) {
            throw new RoomNotFoundException(reservationDto.RoomId);
        }
        if(reservationDto.TenantId!.Equals(room.LandlordId)) {
            throw new CustomizeException(Invalidate.TenantIdAndLandlordIdDuplication);
        }

        var reservations = await _repository.Reservation.FindCondition(r =>
            r.RoomId!.Equals(reservationDto.RoomId)
        ).ToListAsync();

        foreach(var reservation in reservations) {
            var date = reservation.ReservationDate;
            var isDuplicate =
                date.Year == reservationDto.ReservationDate.Year &&
                date.Month == reservationDto.ReservationDate.Month &&
                date.Day == reservationDto.ReservationDate.Day &&
                date.Hour == reservationDto.ReservationDate.Hour &&
                date.Minute == reservationDto.ReservationDate.Minute;
            if(isDuplicate) {
                return new Response
                {
                    Success = true,
                    Messages = Invalidate.ReservationDateDuplicate,
                    StatusCode = (int)HttpStatusCode.ResetContent
                };
            }

        }

        var reservationInsert = _mapper.Map<Reservation>(reservationDto);
        _repository.Reservation.Create(reservationInsert);
        await _repository.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Messages = Successfull.InsertSucceed,
            StatusCode = (int)HttpStatusCode.Created
        };
    }

    public async Task<Response> UpdateAsync(string id, ReservationUpdateDto reservationUpdateDto)
    {
        var reservation = await _repository.Reservation.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if(reservation != null) {
            _mapper.Map(reservationUpdateDto, reservation);
            reservation.UpdatedAt = DateTime.Now;
            _repository.Reservation.Update(reservation);
            await _repository.SaveChangesAsync();
        }
        else {
            throw new ReservationNotFoundException(id);
        }

        return new Response
        {
            Success = true,
            Messages = Successfull.UpdateSucceed,
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> ResponseAsync(string id, ReservationEnums.StatusReservation status, string? rejectionReason)
    {
        var reservation = await _repository.Reservation.FindCondition(r => r.Id.Equals(id))
          .FirstOrDefaultAsync();

        if(reservation == null)
            throw new ReservationNotFoundException(id);

        reservation.Status = status;
        if(rejectionReason != null) {
            reservation.RejectionReason = rejectionReason;
        }

        _repository.Reservation.Update(reservation);
        await _repository.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Messages = Successfull.ResponseSucceed,
            StatusCode = (int)HttpStatusCode.OK
        };
    }
    #endregion
}