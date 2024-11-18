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
using Core.Params;
using Core.Specifications;
using Core.Consts;

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

        public async Task<Response> GetDetailAsync(string id)
        {
            var spec = new BaseSpecification<User>(p =>
                p.Id.Equals(id)
            );
            spec.AddInclude(x => x
                .Include(u => u.Posts!)
                .ThenInclude(p => p.Room!)
                .ThenInclude(r => r.Photos)
            );

            var user = await _repository.User.GetEntityWithSpec(spec);
            return new Response
            {
                Success = true,
                Data = _mapper.Map<UserDto>(user),
                StatusCode = user is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetListAsync(UserSpecParams specParams)
        {
            var spec = new UserSpecification(specParams);
            var page = await CreatePagedResult(spec, specParams.PageIndex, specParams.PageSize);
            return new Response
            {
                Success = true,
                Data = new
                {
                    page.PageIndex,
                    page.PageSize,
                    page.Count,
                    Data = _mapper.Map<IEnumerable<UserDto>>(page.Data)
                },
                StatusCode = !page.Data.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetListCelebrityAsync()
        {
            var spec = new BaseSpecification<User>(u => u
                .UserRoles.Any(ur => RoleConst.Landlord.Contains(ur.Role!.Name))
            );
            spec.AddInclude(x => x
                .Include(p => p.Followers!)
            );
            spec.AddOrder(x => x
                .OrderByDescending(p => p.Followers.Count));
            spec.ApplyPaging(0, 6);

            var users = await _repository.User.ListAsync(spec);
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<UserDto>>(users),
                StatusCode = !users.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
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
                throw new UserNotFoundException();
            }
        }

        public async Task<Response> UpdateAsync(string id, UserUpdateDto? userUpdateDto, IFormFile? file)
        {
            var user = await _repository.User.FindCondition(u => u.Id.Equals(id))
                .SingleOrDefaultAsync();
            if (user != null)
            {
                if (userUpdateDto != null)
                {
                    await ValidateObjectUpdate(id, userUpdateDto);
                    _mapper.Map(userUpdateDto, user);
                }
                if (file != null)
                {
                    await SetAvatar(user, file);
                }
                user.UpdatedAt = DateTime.Now;
                _repository.User.Update(user);
                await _repository.SaveAsync();
            }
            else
            {
                throw new UserNotFoundException();
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
        public async Task<Response> GetSearchOptionsAsync()
        {
            var spec = new BaseSpecification<User>(u => u
                .UserRoles.Any(ur => RoleConst.Landlord.Contains(ur.Role!.Name))
            );
            spec.AddInclude(x => x
                .Include(p => p.UserRoles!)
                .ThenInclude(r => r.Role!)
            );

            var options = await _repository.User.ListAsync(spec);
            return new Response
            {
                Success = true,
                Data = options.Select(u => new
                {
                    u.FullName,
                    u.Address
                }).SelectMany(o => new[] { o.FullName, o.Address }),
                StatusCode = !options.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task ValidateObjectUpdate(string id, UserUpdateDto userUpdateDto)
        {
            var isDuplicateUserNumberPhone = await _repository.User.FindCondition(u => u.PhoneNumber.Equals(userUpdateDto.PhoneNumber)).FirstOrDefaultAsync();
            if (isDuplicateUserNumberPhone != null && isDuplicateUserNumberPhone.Id != id)
            {
                throw new CustomizeException(Invalidate.NumberPhoneDuplication);
            }
        }
        #endregion

    }
}
