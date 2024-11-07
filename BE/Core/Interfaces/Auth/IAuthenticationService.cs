using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="registerDto">Thông tin đăng ký</param>
        /// <returns>Tài khoản và mật khẩu đăng nhập</returns>
        Task<Response> Register(RegisterDto registerDto);
        /// <summary>
        /// Đăng nhập tài khoản
        /// </summary>
        /// <param name="loginDto">Thông tin đăng nhập</param>
        /// <returns>
        /// 1 - Đăng nhập thành công
        /// 2 - Đăng nhập thất bại
        /// </returns>
        Task<Response> Login(LoginDto loginDto);
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="passwordDto">
        /// - Mật khẩu cũ
        /// - Mật khẩu mới
        /// </param>
        /// <returns>
        /// 1 - Đổi mật khẩu thành công
        /// 2 - Đổi mật khẩu thất bại
        /// </returns>
        Task<Response> ChangePassword(ChangePasswordDto passwordDto);

        /// <summary>
        /// Token xác thực
        /// </summary>
        /// <param name="populateExp">Gia hạn làm mới?</param>
        /// <returns>TokenAccess và TokenRefresh</returns>
        Task<TokenDto> CreateToken(bool populateExp);
        /// <summary>
        /// Làm mới Token
        /// </summary>
        /// <param name="tokenDto">
        /// - TokenAccess
        /// - TokenRefresh
        /// </param>
        /// <returns>TokenDto</returns>
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        /// <summary>
        /// Lấy người dùng hiện tại
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <returns>
        /// 1 - Người dùng
        /// 2 - Null
        /// </returns>
        Task<Response> UserCurrent(string username);
        /// <summary>
        /// Xác thực email
        /// </summary>
        /// <param name="email">Email người dùng</param>
        /// <param name="token">Chuỗi xác thực</param>
        /// <returns>
        /// 1 - Xác thực thành công
        /// 2 - Xác thực thất bại
        /// </returns>
        Task<Response> EmailConfirm(string email, string token);
        /// <summary>
        /// Gửi email xác thực
        /// </summary>
        /// <param name="email">Email người dùng</param>
        /// <returns>Gửi Email xác thực thành công</returns>
        Task<Response> SendEmailConfirm(string email);
        /// <summary>
        /// Gửi Email quên mật khẩu
        /// </summary>
        /// <param name="email">Tài khoản Email</param>
        /// <returns>
        /// Gửi Email quên mật khẩu thành công
        /// </returns>
        Task<Response> SendEmailForgot(string email);
        /// <summary>
        /// Đặt mật khẩu mới
        /// </summary>
        /// <param name="resetPasswordDto">
        /// - Email
        /// - Mật khẩu mới
        /// - Token
        /// </param>
        /// <returns>
        /// Thông báo đổi mật khẩu thành công
        /// </returns>
        Task<Response> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
