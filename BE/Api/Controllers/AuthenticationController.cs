using Core.Dtos;
using Core.Extensions;
using Core.Interfaces.Business;
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
        /// Xác nhận Email
        /// </summary>
        [HttpGet("email-confirm")]
        public async Task<IActionResult> EmailConfirm(string email, string token)
        {
            var response = await _service.Authentication.EmailConfirm(email, token);
            return Ok(response);
        }
        /// <summary>
        /// Gửi Email xác thực
        /// </summary>
        [HttpGet("send-email-confirm")]
        public async Task<IActionResult> ResendEmailConfirmationLink(string email)
        {
            var response = await _service.Authentication.SendEmailConfirm(email);
            return Ok(response);
        }
        /// <summary>
        /// Gửi Email quên mật khẩu
        /// </summary>
        [HttpGet("send-email-forgot")]
        public async Task<IActionResult> SendEmailForgot(string email)
        {
            var response = await _service.Authentication.SendEmailForgot(email);
            return Ok(response);
        }
        /// <summary>
        /// Đặt mật khẩu mới
        /// </summary>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var response = await _service.Authentication.ResetPassword(resetPasswordDto);
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
