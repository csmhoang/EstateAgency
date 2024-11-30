using Core.Dtos;
using Core.Extensions;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
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
        /// Lấy thông tin giỏ hàng
        /// </summary>
        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> CurrentCart()
        {
            var userId = User.GetUserId();
            var response = await _service.Cart.CartCurrent(userId);
            return Ok(response);
        }
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
        public async Task<IActionResult> Get(string cartId)
        {
            var response = await _service.Cart.GetAsync(cartId);
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

        /// <summary>
        /// Cập nhật giỏ phòng
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update(string cartId, [FromBody] CartDto model)
        {
            var response = await _service.Cart.UpdateAsync(cartId, model);
            return Ok(response);
        }
        #endregion
    }
}
