﻿using Core.Dtos;
using Core.Interfaces.Business;
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
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Post.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin bài đăng bằng id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Post.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Thêm bài đăng
        /// </summary>
        /// <param name="model">Bài đăng</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostDto model)
        {
            var response = await _service.Post.InsertAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật bài đăng
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <param name="model">Bài đăng</param>
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] PostDto model)
        {
            var response = await _service.Post.UpdateAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa bài đăng
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Post.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
