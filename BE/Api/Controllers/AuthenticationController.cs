﻿using Core.Dtos;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public AuthenticationController(IServiceManager service)
        {
            _service = service;
        }
        #endregion

        #region Method
        /// <summary>
        /// Đăng ký
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            var response = await _service.Authentication.Register(registerDto);
            return Ok(response);
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var response = await _service.Authentication.Login(loginDto);
            return Ok(response);
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        [HttpPost("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto passwordDto)
        {
            var response = await _service.Authentication.ChangePassword(passwordDto);
            return Ok(response);
        }
        #endregion
    }
}