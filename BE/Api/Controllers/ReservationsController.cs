using Core.Consts;
using Core.Dtos;
using Core.Interfaces.Business;
using Core.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ReservationsController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin đặt lịch
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Reservation.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin đặt lịch bằng id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Reservation.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách thông tin đặt lịch bằng specification
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] ReservationSpecParams specParams)
        {
            var response = await _service.Reservation.GetListAsync(specParams);
            return Ok(response);
        }

        /// <summary>
        /// Thêm đặt lịch
        /// </summary>
        /// <param name="model">Đặt lịch</param>
        [HttpPost]
        [Authorize(Roles = RoleConst.Tenant)]
        public async Task<IActionResult> Create([FromBody] ReservationDto model)
        {
            var response = await _service.Reservation.InsertAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <param name="model">Đặt lịch</param>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] ReservationUpdateDto model)
        {
            var response = await _service.Reservation.UpdateAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Reservation.DeleteAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Từ chối đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <param name="rejectionReason">Lý do từ chối</param>
        [HttpPut("refuse")]
        [Authorize(Roles = RoleConst.Landlord)]
        public async Task<IActionResult> Refuse(string id, string rejectionReason)
        {
            var response = await _service.Reservation.RefuseAsync(id, rejectionReason);
            return Ok(response);
        }

        /// <summary>
        /// Chấp nhận đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        [HttpPut("accept")]
        [Authorize(Roles = RoleConst.Landlord)]
        public async Task<IActionResult> Accept(string id)
        {
            var response = await _service.Reservation.AcceptAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
