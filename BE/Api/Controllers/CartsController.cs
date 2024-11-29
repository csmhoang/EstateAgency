using Core.Dtos;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public CartsController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin chi tiết giỏ phòng
        /// </summary>
        [HttpGet("detail")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Cart.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin chi tiết giỏ phòng bằng id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Cart.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Thêm phòng vào giỏ
        /// </summary>
        [HttpPost("append")]
        public async Task<IActionResult> Append([FromBody] CartDetailDto model)
        {
            var response = await _service.Cart.AppendAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa phòng khỏi giỏ
        /// </summary>
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(string cartDetailId)
        {
            var response = await _service.Cart.RemoveAsync(cartDetailId);
            return Ok(response);
        }
        #endregion
    }
}
