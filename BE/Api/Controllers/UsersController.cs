using Core.Consts;
using Core.Dtos;
using Core.Interfaces.Business;
using Core.Params;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public UsersController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin người dùng
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.User.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin người dùng bằng id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.User.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin chi tiết người dùng bằng id
        /// </summary>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetail(string id)
        {
            var response = await _service.User.GetDetailAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách thông tin người dùng bằng specification
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] UserSpecParams specParams)
        {
            var response = await _service.User.GetListAsync(specParams);
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách thông tin người cho thuê được theo dõi nhiều nhất
        /// </summary>
        [HttpGet("famous")]
        public async Task<IActionResult> GetListCelebrity()
        {
            var response = await _service.User.GetListCelebrityAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách gợi ý cho bộ tìm kiếm
        /// </summary>
        [HttpGet("search-options")]
        public async Task<IActionResult> GetSearchOptions()
        {
            var response = await _service.User.GetSearchOptionsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Theo dõi người dùng
        /// </summary>
        [HttpPost("follow")]
        [Authorize]
        public async Task<IActionResult> Follow(string followerId, string followeeId, bool isFollow)
        {
            var response = await _service.User.FollowAsync(followerId, followeeId, isFollow);
            return Ok(response);
        }
        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <param name="model">Người dùng</param>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] UserUpdateDto model)
        {
            var response = await _service.User.UpdateAsync(id, model, null);
            return Ok(response);
        }
        /// <summary>
        /// Cập nhật Avatar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        [HttpPut("avatar")]
        [Authorize]
        public async Task<IActionResult> SetAvatar(string id, IFormFile file)
        {
            var response = await _service.User.UpdateAsync(id, null, file);
            return Ok(response);
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="id">Id người dùng</param>
        [HttpDelete]
        [Authorize(Roles = RoleConst.Admin)]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.User.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
