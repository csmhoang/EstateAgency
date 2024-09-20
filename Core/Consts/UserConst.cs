using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Consts
{
    public static class UserConst
    {
        public const string ErrorEmptyFullName = "Họ tên người dùng không được phép để trống!";

        public const string ErrorEmptyEmail = "Email không được phép để trống!";

        public const string ErrorEmptyUserName = "Tên đăng nhập người dùng không được phép để trống!";

        public const string ErrorEmptyPassword = "Mật khẩu không được phép để trống!";

        public const string ErrorEmptyNewPassword = "Mật khẩu mới không được phép để trống!";

        public const string ErrorLengthPassword = "Mật khẩu phải có ít nhất {2} ký tự, nhiều nhất {1} ký tự!";

        public const string ErrorFormatEmail = "Email không hợp lệ!";

    }
}
