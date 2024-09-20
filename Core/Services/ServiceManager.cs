using AutoMapper;
using Core.Entities;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServiceManager : IServiceManager
    {
        #region Declaration
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<Interfaces.Auth.IAuthenticationService> _authenticationService;
        private readonly Lazy<IRoomService> _roomService;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ServiceManager(
            IRepositoryManager repository,
            ILoggerManager logger,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userService = new Lazy<IUserService>(() =>
                new UserService(repository, logger, mapper));
            _authenticationService = new Lazy<Interfaces.Auth.IAuthenticationService>(() =>
                new AuthenticationService(logger, mapper, userManager, configuration));
            _roomService = new Lazy<IRoomService>(() =>
                new RoomService(repository, logger, mapper));
        }
        #endregion

        #region Method
        public IUserService User => _userService.Value;
        public Interfaces.Auth.IAuthenticationService Authentication
            => _authenticationService.Value;
        public IRoomService Room => _roomService.Value;
        #endregion
    }
}
