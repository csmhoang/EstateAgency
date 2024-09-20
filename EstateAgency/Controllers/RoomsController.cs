using Core.Dtos;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public RoomsController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin phòng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Room.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin phòng bằng id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Room.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Thêm phòng
        /// </summary>
        /// <param name="model">Phòng</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomDto model)
        {
            var response = await _service.Room.InsertAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật phòng
        /// </summary>
        /// <param name="id">Id phòng</param>
        /// <param name="landlordId">Id chủ phòng</param>
        /// <param name="model">Phòng</param>
        [HttpPut]
        public async Task<IActionResult> Update(string id, string? landlordId, [FromBody] RoomDto model)
        {
            var response = await _service.Room.UpdateAsync(id, landlordId, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa phòng
        /// </summary>
        /// <param name="id">Id phòng</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Room.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
