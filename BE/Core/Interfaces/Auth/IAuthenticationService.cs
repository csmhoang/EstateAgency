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

    }
}
