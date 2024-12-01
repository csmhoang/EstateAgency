using Core.Consts;
using Core.Dtos;
using Core.Extensions;
using Core.Interfaces.Business;
using Core.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Core.Enums.BookingEnums;

namespace Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public BookingsController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin đặt phòng
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Booking.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin đặt phòng bằng id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Booking.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách thông tin đặt phòng bằng specification
        /// </summary>
        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetList([FromQuery] BookingSpecParams specParams)
        {
            var response = await _service.Booking.GetListAsync(specParams);
            return Ok(response);
        }

        /// <summary>
        /// Thêm đặt phòng từ giỏ phòng
        /// </summary>
        [HttpPost]
        [Authorize(Roles = RoleConst.Tenant)]
        public async Task<IActionResult> Create()
        {
            var userId = User.GetUserId();
            var response = await _service.Booking.InsertAsync(userId);
            return Ok(response);
        }

        /// <summary>
        /// Hủy đặt phòng
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        /// <param name="status">Trạng thái</param>
        [HttpDelete("response")]
        [Authorize]
        public async Task<IActionResult> ResponseRequest(string id, StatusBooking status)
        {
            var response = await _service.Booking.ResponseAsync(id, status);
            return Ok(response);
        }

        /// <summary>
        /// Phản hồi chi tiết đặt phòng
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        /// <param name="status">Trạng thái</param>
        /// <param name="rejectionReason">Lý do từ chối</param>
        [HttpPut("response-detail")]
        [Authorize]
        public async Task<IActionResult> ResponseDetail(string id, StatusBookingDetail status, string? rejectionReason)
        {
            var response = await _service.Booking.ResponseDetailAsync(id, status, rejectionReason);
            return Ok(response);
        }
        #endregion
    }
}
