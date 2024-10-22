using Api.Extensions;
using Core.Consts;
using Core.Dtos;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
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
        /// Lấy thông tin người dùng đăng nhập
        /// </summary>
        [HttpGet("current")]
        public async Task<IActionResult> CurrentUser()
        {
            var username = User.GetUsername();
            var response = await _service.Authentication.UserCurrent(username);
            return Ok(response);
        }
        /// <summary>
        /// Đăng ký
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = await _service.Authentication.Register(registerDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _service.Authentication.Login(loginDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        [HttpPost("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto passwordDto)
        {
            var response = await _service.Authentication.ChangePassword(passwordDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        #endregion
    }
}
