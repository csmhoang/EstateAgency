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
            var post = await _repository.Post.FindCondition(p =>
               p.Id.Equals(bookingDto.PostId)
           ).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new PostNotFoundException(bookingDto.PostId);
            }
            if (bookingDto.TenantId!.Equals(post.LandlordId))
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

        public async Task<Response> UpdateAsync(string id, BookingDto bookingDto)
        {
            await ValidateObject(bookingDto);

            var booking = await _repository.Booking.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (booking is not null)
            {
                _mapper.Map(bookingDto, booking);
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
        public Task ValidateObject(BookingDto bookingDto)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
