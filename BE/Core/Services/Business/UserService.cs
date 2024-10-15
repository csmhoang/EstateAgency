using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Resources;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Http;
using Core.Services.Infrastructure;

namespace Core.Services.Business
{
    internal sealed class UserService : ServiceBase<User>, IUserService
    {
        #region Declaration
        private readonly IRepositoryManager _repository;
        private readonly IPhotoService _photoService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public UserService(
            IRepositoryManager repository,
            IPhotoService photoService,
            ILoggerManager logger,
            IMapper mapper) : base(repository.User)
        {
            _repository = repository;
            _photoService = photoService;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Response> GetAllAsync()
        {
            var users = await _repository.User.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<UserDto>>(users),
                StatusCode = !users.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var user = await _repository.User.FindCondition(u => u.Id.Equals(id))
                .SingleOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<UserDto>(user),
                StatusCode = user is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        public async Task<Response> DeleteAsync(string id)
        {
            var userDelete = await _repository.User.FindCondition(e => e.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (userDelete is not null)
            {
                _repository.User.Delete(userDelete);
                await _repository.SaveAsync();
                return new Response
                {
                    Success = true,
                    Messages = Successfull.DeleteSucceed,
                    StatusCode = (int)HttpStatusCode.NoContent
                };
            }
            else
            {
                throw new UserNotFoundException(id);
            }
        }

        public async Task<Response> UpdateAsync(string id, UserDto userDto, IFormFile? file)
        {
            await ValidateObject(userDto);

            var user = await _repository.User.FindCondition(u => u.Id.Equals(id))
                .SingleOrDefaultAsync();
            if (user != null)
            {
                _mapper.Map(userDto, user);
                if (file != null)
                {
                    await SetAvatar(user, file);
                }
                _repository.User.Update(user);
                await _repository.SaveAsync();
            }
            else
            {
                throw new UserNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public async Task SetAvatar(User user, IFormFile file)
        {
            var uploadPhotoResult =
                await _photoService.UploadPhotoAsync(file);
            if (uploadPhotoResult.Error != null)
            {
                throw new CustomizeException(Failure.UploadPhotoFailing);
            }
            if (!string.IsNullOrEmpty(user.PublicId))
            {
                var deletePhotoResult =
                    await _photoService.DeletePhotoAsync(user.PublicId);
                if (deletePhotoResult.Error != null || deletePhotoResult.Result == "not found")
                {
                    throw new CustomizeException(Failure.DeletePhotoFailing);
                }
            }
            user.AvatarUrl = uploadPhotoResult.SecureUrl.AbsoluteUri;
            user.PublicId = uploadPhotoResult.PublicId;
        }
        public async Task ValidateObject(UserDto userDto)
        {
            var isDuplicateUserEmail = await _repository.User.FindCondition(u => u.Email.Equals(userDto.Email)).FirstOrDefaultAsync();
            if (isDuplicateUserEmail is not null)
            {
                throw new CustomizeException(Invalidate.EmailDuplication);
            }
            var isDuplicateUserNumberPhone = await _repository.User.FindCondition(u => u.PhoneNumber.Equals(userDto.PhoneNumber)).FirstOrDefaultAsync();
            if (isDuplicateUserNumberPhone is not null)
            {
                throw new CustomizeException(Invalidate.NumberPhoneDuplication);
            }
        }
        #endregion

    }
}
