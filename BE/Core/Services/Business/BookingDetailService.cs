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
using System.Net;
using static Core.Enums.InvoiceEnums;
using static Core.Enums.LeaseEnums;

namespace Core.Services.Business
{
    internal class BookingDetailService : ServiceBase<BookingDetail>, IBookingDetailService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public BookingDetailService(IRepositoryManager repository,
                   ILoggerManager logger,
                   IMapper mapper) : base(repository.BookingDetail)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var bookingDetails = await _repository.BookingDetail.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<BookingDetailDto>>(bookingDetails),
                StatusCode = !bookingDetails.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var bookingDetail = await _repository.BookingDetail.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<BookingDetailDto>(bookingDetail),
                StatusCode = bookingDetail is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetRentedBookingDetailsAsync(BookingDetailSpecParams specParams)
        {
            var spec = new BookingDetailSpecification(
                specParams,
                x => x.Booking!.Lease!.Status == StatusLease.Active
            );

            var page = await CreatePagedResult(spec, specParams.PageIndex, specParams.PageSize);
            return new Response
            {
                Success = true,
                Data = new
                {
                    page.PageIndex,
                    page.PageSize,
                    page.Count,
                    Data = _mapper.Map<IEnumerable<BookingDetailDto>>(page.Data)
                },
                StatusCode = !page.Data.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        #endregion
    }
}
