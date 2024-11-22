using Core.Dtos;
using Core.Interfaces.Business;
using Core.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Booking.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin đặt phòng bằng id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Booking.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách thông tin đặt phòng bằng specification
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] BookingSpecParams specParams)
        {
            var response = await _service.Booking.GetListAsync(specParams);
            return Ok(response);
        }

        /// <summary>
        /// Thêm đặt phòng
        /// </summary>
        /// <param name="model">Đặt phòng</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDto model)
        {
            var response = await _service.Booking.InsertAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật đặt phòng
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        /// <param name="model">Đặt phòng</param>
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] BookingUpdateDto model)
        {
            var response = await _service.Booking.UpdateAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa đặt phòng
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Booking.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
