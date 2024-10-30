using Core.Dtos;
using Core.Interfaces.Business;
using Core.Params;
using Core.Specifications;
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
        /// Lấy danh sách thông tin phòng bằng specification
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] RoomSpecParams specParams)
        {
            var response = await _service.Room.GetListAsync(specParams);
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
        /// <param name="files">Danh sách tệp ảnh</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RoomDto model, IFormFile[]? files)
        {
            var response = await _service.Room.InsertAsync(model, files);
            return Ok(response);
        }

        /// <summary>
        /// Thêm ảnh cho phòng
        /// </summary>
        /// <param name="roomId">Mã phòng</param>
        /// <param name="file">Tệp ảnh</param>
        [HttpPost("insert-photo/{roomId}")]
        public async Task<IActionResult> InsertPhoto(string roomId, IFormFile file)
        {
            var response = await _service.Room.InsertPhotoAsync(roomId, file);
            return Ok(response);
        }

        /// <summary>
        /// Xóa ảnh phòng
        /// </summary>
        /// <param name="roomId">Mã phòng</param>
        /// <param name="photoId">Mã ảnh</param>
        [HttpDelete("delete-photo")]
        public async Task<IActionResult> DeletePhoto(string roomId, string photoId)
        {
            var response = await _service.Room.DeletePhotoAsync(roomId, photoId);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật phòng
        /// </summary>
        /// <param name="id">Id phòng</param>
        /// <param name="model">Phòng</param>
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] RoomDto model)
        {
            var response = await _service.Room.UpdateAsync(id, model);
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
