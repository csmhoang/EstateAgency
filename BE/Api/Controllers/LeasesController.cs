using Core.Dtos;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Core.Enums.InvoiceEnums;
using static Core.Enums.LeaseEnums;

namespace Api.Controllers
{
    [Route("api/v1/leases")]
    [ApiController]
    public class LeasesController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public LeasesController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin hợp đồng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Lease.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin hợp đồng bằng id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Lease.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin hợp đồng bằng bookingId
        /// </summary>
        [HttpGet("bookingId/{bookingId}")]
        public async Task<IActionResult> GetByBookingId(string bookingId)
        {
            var response = await _service.Lease.GetByBookingIdAsync(bookingId);
            return Ok(response);
        }

        /// <summary>
        /// Thêm hợp đồng
        /// </summary>
        /// <param name="model">Hợp đồng</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeaseDto model)
        {
            var response = await _service.Lease.InsertAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Phản hồi hợp đồng
        /// </summary>
        /// <param name="id">Id hợp đồng</param>
        /// <param name="status">Trạng thái</param>
        [HttpPut("response")]
        [Authorize]
        public async Task<IActionResult> ResponseRequest(string id, StatusLease status)
        {
            var response = await _service.Lease.ResponseAsync(id, status);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật hợp đồng
        /// </summary>
        /// <param name="id">Id hợp đồng</param>
        /// <param name="model">Hợp đồng</param>
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] LeaseDto model)
        {
            var response = await _service.Lease.UpdateAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa hợp đồng
        /// </summary>
        /// <param name="id">Id hợp đồng</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Lease.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
