using Core.Consts;
using Core.Dtos;
using Core.Interfaces.Business;
using Core.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public PostsController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin bài đăng
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Post.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách thông tin bài đăng bằng specification
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] PostSpecParams specParams)
        {
            var response = await _service.Post.GetListAsync(specParams);
            return Ok(response);
        }
        /// <summary>
        /// Lấy danh sách bài đăng mới
        /// </summary>
        [HttpGet("recent")]
        public async Task<IActionResult> GetListRecent()
        {
            var response = await _service.Post.GetListRecentAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin bài đăng bằng id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Post.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin chi tiết bài đăng bằng id
        /// </summary>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetail(string id)
        {
            var response = await _service.Post.GetDetailAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách gợi ý cho bộ tìm kiếm
        /// </summary>
        [HttpGet("search-options")]
        public async Task<IActionResult> GetSearchOptions()
        {
            var response = await _service.Post.GetSearchOptionsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Thêm bài đăng
        /// </summary>
        /// <param name="model">Bài đăng</param>
        [HttpPost]
        [Authorize(Roles = RoleConst.Landlord)]
        public async Task<IActionResult> Create([FromBody] PostDto model)
        {
            var response = await _service.Post.InsertAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Lưu bài đăng
        /// </summary>
        /// <param name="model">Thông tin lưu</param>
        /// <param name="isSave">Lưu hoặc gỡ khỏi</param>
        [HttpPost("save")]
        [Authorize]
        public async Task<IActionResult> SavePost([FromBody] SavePostDto model, bool isSave)
        {
            var response = await _service.Post.SavePostAsync(model, isSave);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật bài đăng
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <param name="model">Bài đăng</param>
        [HttpPut]
        [Authorize(Roles = $"{RoleConst.Landlord},{RoleConst.Admin}")]
        public async Task<IActionResult> Update(string id, [FromBody] PostUpdateDto model)
        {
            var response = await _service.Post.UpdateAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa bài đăng
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        [HttpDelete]
        [Authorize(Roles = $"{RoleConst.Landlord},{RoleConst.Admin}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Post.DeleteAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Gỡ bài đăng
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        [HttpPut("remove")]
        [Authorize(Roles = $"{RoleConst.Landlord},{RoleConst.Admin}")]
        public async Task<IActionResult> Remove(string id)
        {
            var response = await _service.Post.RemoveAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
