using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Params;
using Core.Resources;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.BookingEnums;
using static Core.Enums.ReservationEnums;

namespace Core.Services.Business
{
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
        public async Task<Response> DeleteAsync(string id)
        {
            var bookingDelete = await _repository.Booking.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (bookingDelete is not null)
            {
                _repository.Booking.Delete(bookingDelete);
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
                throw new BookingNotFoundException(id);
            }
        }
        public async Task<Response> InsertAsync(BookingDto bookingDto)
        {
            var room = await _repository.Room.FindCondition(p =>
               p.Id.Equals(bookingDto.RoomId)
           ).FirstOrDefaultAsync();
            if (room == null)
            {
                throw new RoomNotFoundException(bookingDto.RoomId);
            }
            if (bookingDto.TenantId!.Equals(room.LandlordId))
            {
                throw new CustomizeException(Invalidate.TenantIdAndLandlordIdDuplication);
            }

            var booking = _mapper.Map<Booking>(bookingDto);
            _repository.Booking.Create(booking);
            await _repository.SaveAsync();

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> UpdateAsync(string id, BookingUpdateDto bookingUpdateDto)
        {
            var booking = await _repository.Booking.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (booking is not null)
            {
                _mapper.Map(bookingUpdateDto, booking);
                _repository.Booking.Update(booking);
                await _repository.SaveAsync();
            }
            else
            {
                throw new BookingNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> RefuseAsync(string id, string rejectionReason)
        {
            var booking = await _repository.Booking.FindCondition(r => r.Id.Equals(id))
              .FirstOrDefaultAsync();
            if (booking != null)
            {
                booking.Status = StatusBooking.Rejected;
                booking.RejectionReason = rejectionReason;
                _repository.Booking.Update(booking);
                await _repository.SaveAsync();
            }
            else
            {
                throw new BookingNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.RejectSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> AcceptAsync(string id)
        {
            var booking = await _repository.Booking.FindCondition(r => r.Id.Equals(id))
              .FirstOrDefaultAsync();
            if (booking != null)
            {
                booking.Status = StatusBooking.Accepted;
                _repository.Booking.Update(booking);
                await _repository.SaveAsync();
            }
            else
            {
                throw new BookingNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.AcceptSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public Task ValidateObject(BookingDto bookingDto)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
