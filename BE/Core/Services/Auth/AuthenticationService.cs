using AutoMapper;
using Core.Consts;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Infrastructure;
using Core.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services.Auth
{
    internal sealed class AuthenticationService : Interfaces.Auth.IAuthenticationService

    {
        #region Declaration
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        #endregion

        #region Property
        private User? _user;
        #endregion

        #region Constructor
        public AuthenticationService(ILoggerManager logger,
            IMapper mapper,
            IEmailSender emailSender,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        #endregion

        #region Method
        public async Task<Response> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email.ToLower()))
            {
                throw new UserExistedException(registerDto.Email.ToLower());
            }

            var roles = registerDto.Roles;
            if (!(roles == null || !roles.Any()))
            {
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        throw new RoleNotFoundException(role);
                    }
                }
            }

            var user = _mapper.Map<User>(registerDto);
            user.UserName = registerDto.Email.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            var res = new Response();

            if (result.Succeeded)
            {
                try
                {
                    if (!(roles == null || !roles.Any()))
                    {
                        var roleResult = await _userManager.AddToRolesAsync(user, roles);
                        if (!roleResult.Succeeded)
                        {
                            await _userManager.DeleteAsync(user);
                            throw new CustomizeException(Failure.AssignRoleFailing);
                        }
                    }
                    else
                    {
                        var defaultRoleResult = await _userManager.AddToRoleAsync(user, RoleConst.Tenant);
                        if (!defaultRoleResult.Succeeded)
                        {
                            await _userManager.DeleteAsync(user);
                            throw new CustomizeException(Failure.AssignRoleFailing);
                        }
                    }
                }
                catch
                {
                    await _userManager.DeleteAsync(user);
                    throw new CustomizeException(Failure.AddRoleFailing);
                }

                res.Success = true;
                res.Messages = Successfull.RegisterSucceed;
                res.StatusCode = (int)HttpStatusCode.Created;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    res.Errors.Add($"{error.Code}: {error.Description}");
                }
                res.Success = false;
                res.Messages = Failure.RegisterFailing;
                res.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return res;
        }

        public async Task<Response> EmailConfirm(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (result.Succeeded)
            {
                return new Response
                {
                    Success = true,
                    Messages = Successfull.EmailConfirmSucceed,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            return new Response
            {
                Success = false,
                Messages = Failure.EmailConfirmFailing,
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }

        public async Task<Response> SendEmailConfirm(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodeEmailToken = Encoding.UTF8.GetBytes(token);
            var ValidateEmailToken = WebEncoders.Base64UrlEncode(encodeEmailToken);
            var message = $"<p>Email Confirm Token: {ValidateEmailToken}</p>";
            await _emailSender.SendEmailAsync(user.Email, "Xác thực Email", message);

            return new Response
            {
                Success = true,
                Messages = Successfull.SendEmailSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> SendEmailForgot(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodeForgotToken = Encoding.UTF8.GetBytes(token);
            var ValidateForgotToken = WebEncoders.Base64UrlEncode(encodeForgotToken);
            var message = $"<p>Reset Password Token: {ValidateForgotToken}</p>";
            await _emailSender.SendEmailAsync(user.Email, "Đặt lại mật khẩu", message);

            return new Response
            {
                Success = true,
                Messages = Successfull.SendEmailSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> ResetPassword(ResetPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var decodedTokenBytes = WebEncoders.Base64UrlDecode(forgotPasswordDto.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, forgotPasswordDto.NewPassword);

            if (result.Succeeded)
            {
                return new Response
                {
                    Success = true,
                    Messages = Successfull.ChangePasswordSucceed,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            return new Response
            {
                Success = false,
                Messages = Failure.ChangePasswordFailing,
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }

        public async Task<Response> Login(LoginDto loginDto)
        {
            _user = await _userManager.FindByNameAsync(loginDto.Email.ToLower());
            var result =
                _user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password);
            if (!result)
            {
                _logger.LogWarn($"{nameof(Login)}: {Failure.LoginFailing}");
            }
            return new Response
            {
                Success = result,
                Data = result ? await CreateToken(true) : null,
                Messages = result ? Successfull.LoginSucceed : Failure.LoginFailing,
                StatusCode = result ? (int)HttpStatusCode.OK : (int)HttpStatusCode.Unauthorized
            };
        }
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();

            _user!.RefreshToken = refreshToken;
            if (populateExp)
            {
                _user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            }

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto(accessToken, refreshToken);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, _user!.Id),
                new Claim(ClaimTypes.Name, _user!.UserName)
            };
            var userRoles = await _userManager.GetRolesAsync(_user!);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return authClaims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _configuration["JwtSettings:validIssuer"],
                audience: _configuration["JwtSettings:validAudience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:expires"])),
                claims: claims,
                signingCredentials: signingCredentials
            );
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["JwtSettings:secret"])),
                ValidIssuer = _configuration["JwtSettings:validIssuer"],
                ValidAudience = _configuration["JwtSettings:validAudience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(
                token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token không hợp lệ!");
            }
            return principal;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity?.Name);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new RefreshTokenBadrequest();
            }
            _user = user;

            return await CreateToken(populateExp: false);
        }

        public async Task<Response> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByNameAsync(changePasswordDto?.Email);
            var res = new Response();

            var isCheckAccount =
                user != null && await _userManager.CheckPasswordAsync(user, changePasswordDto?.Password);
            if (!isCheckAccount)
            {
                throw new CustomizeException(Invalidate.IncorrectAccount, (int)HttpStatusCode.Unauthorized);
            }

            var result = await _userManager.ChangePasswordAsync(
                user!,
                changePasswordDto?.Password,
                changePasswordDto?.NewPassword
            );

            if (result.Succeeded)
            {
                res.Success = true;
                res.Messages = Successfull.ChangePasswordSucceed;
                res.StatusCode = (int)HttpStatusCode.NoContent;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    res.Errors.Add($"{error.Code}: {error.Description}");
                }
                res.Success = false;
                res.Messages = Failure.ChangePasswordFailing;
                res.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return res;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users
                .AnyAsync(x => x.UserName == username.ToLower());
        }

        public async Task<Response> UserCurrent(string username)
        {
            var user = string.IsNullOrEmpty(username) ? null : await _userManager.Users
                .AsNoTracking()
                .Include(u => u.UserRoles!)
                .ThenInclude(ur => ur.Role!)
                .Include(u => u.Followees!)
                .ThenInclude(f => f.Followee!)
                .Include(u => u.Followers!)
                .Include(u => u.SavePosts!)
                .ThenInclude(s => s.Post!)
                .ThenInclude(p => p.Room!)
                .ThenInclude(r => r.Photos!)
                .Include(u => u.Reservations!)
                .Include(u => u.Bookings!)
                .Include(u => u.Rooms)
                .FirstOrDefaultAsync(x => x.UserName == username.ToLower());

            return new Response
            {
                Success = true,
                Data = _mapper.Map<UserDto>(user),
                StatusCode = user is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        #endregion
    }
}
