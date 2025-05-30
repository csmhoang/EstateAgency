﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core;

internal class BookingService : ServiceBase<Booking>, IBookingService
{
    #region Declaration
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public BookingService(IRepositoryManager repository,
               ILoggerManager logger,
               IMapper mapper) : base(repository.Booking)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    #endregion

    #region Method
    public async Task<Response> GetAllAsync()
    {
        var bookings = await _repository.Booking.FindAll().ToListAsync();
        return new Response
        {
            Success = true,
            Data = _mapper.Map<IEnumerable<BookingDto>>(bookings),
            StatusCode = !bookings.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> GetAsync(string id)
    {
        var booking = await _repository.Booking.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        return new Response
        {
            Success = true,
            Data = _mapper.Map<BookingDto>(booking),
            StatusCode = booking is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }
    public async Task<Response> GetListAsync(BookingSpecParams specParams)
    {
        var spec = new BookingSpecification(specParams);
        var page = await CreatePagedResult(spec, specParams.PageIndex, specParams.PageSize);
        return new Response
        {
            Success = true,
            Data = new
            {
                page.PageIndex,
                page.PageSize,
                page.Count,
                Data = _mapper.Map<IEnumerable<BookingDto>>(page.Data)
            },
            StatusCode = !page.Data.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> ResponseAsync(string id, BookingEnums.StatusBooking status)
    {
        var booking = await _repository.Booking.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (booking == null) throw new BookingNotFoundException(id);
        booking.Status = status;
        booking.UpdatedAt = DateTime.Now;
        _repository.Booking.Update(booking);
        await _repository.SaveChangesAsync();
        return new Response
        {
            Success = true,
            Messages = Successfull.ResponseSucceed,
            StatusCode = (int)HttpStatusCode.NoContent
        };
    }

    public async Task<Response> InsertAsync(string userId)
    {
        var cart = await _repository.Cart.FindCondition(c => c.TenantId!.Equals(userId))
            .Include(c => c.CartDetails!)
            .ThenInclude(cd => cd.Room!)
            .FirstOrDefaultAsync();

        if (cart!.CartDetails == null || cart.CartDetails.Count == 0)
            throw new CustomizeException(Invalidate.CartEmptyInvalidate);

        var CartDetailGroups = cart.CartDetails.GroupBy(cd => cd.Room!.LandlordId);
        var bookings = new List<Booking>();
        foreach (var group in CartDetailGroups)
        {
            var booking = new Booking
            {
                TenantId = cart.TenantId,
            };
            foreach (var cartDetail in group)
            {
                var room = await _repository.Room.FindCondition(p => p.Id.Equals(cartDetail.RoomId))
                    .FirstOrDefaultAsync();
                if (room == null) throw new RoomNotFoundException(cartDetail.RoomId!);
                if (booking.TenantId!.Equals(room.LandlordId))
                    throw new CustomizeException(Invalidate.TenantIdAndLandlordIdDuplication);

                booking.BookingDetails.Add(new BookingDetail
                {
                    RoomId = cartDetail.RoomId!,
                    StartDate = cartDetail.StartDate,
                    EndDate = cartDetail.StartDate.AddMonths(cartDetail.NumberOfMonth),
                    NumberOfMonth = cartDetail.NumberOfMonth,
                    NumberOfTenant = cartDetail.NumberOfTenant,
                    Price = cartDetail.Price
                });
                _repository.CartDetail.Delete(cartDetail);
            }
            bookings.Add(booking);
        }

        foreach (var booking in bookings) _repository.Booking.Create(booking);
        await _repository.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Messages = Successfull.InsertSucceed,
            StatusCode = (int)HttpStatusCode.Created
        };
    }

    public async Task<Response> ResponseDetailAsync(string bookingDetailId, BookingEnums.StatusBookingDetail status, string? rejectionReason)
    {
        var bookingDetail = await _repository.BookingDetail.FindCondition(r => r.Id.Equals(bookingDetailId))
            .FirstOrDefaultAsync();

        if (bookingDetail == null) throw new BookingNotFoundException(bookingDetailId);

        bookingDetail.Status = status;
        if (rejectionReason != null)
        {
            bookingDetail.RejectionReason = rejectionReason;
        }

        _repository.BookingDetail.Update(bookingDetail);
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
