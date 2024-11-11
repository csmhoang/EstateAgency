using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces.Business;
using Core.Interfaces.Data;
using Core.Interfaces.Infrastructure;
using Core.Params;
using Core.Resources;
using Core.Services.Infrastructure;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.Services.Business
{
    internal sealed class RoomService : ServiceBase<Room>, IRoomService
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
        public RoomService(IRepositoryManager repository,
            IPhotoService photoService,
            ILoggerManager logger,
            IMapper mapper) : base(repository.Room)
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
            var rooms = await _repository.Room.FindAll().ToListAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<RoomDto>>(rooms),
                StatusCode = !rooms.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetAsync(string id)
        {
            var room = await _repository.Room.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            return new Response
            {
                Success = true,
                Data = _mapper.Map<RoomDto>(room),
                StatusCode = room is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> GetListAsync(RoomSpecParams specParams)
        {
            var spec = new RoomSpecification(specParams);
            var page = await CreatePagedResult(spec, specParams.PageIndex, specParams.PageSize);
            return new Response
            {
                Success = true,
                Data = new
                {
                    page.PageIndex,
                    page.PageSize,
                    page.Count,
                    Data = _mapper.Map<IEnumerable<RoomDto>>(page.Data)
                },
                StatusCode = !page.Data.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }

        public async Task<Response> DeleteAsync(string id)
        {
            var roomDelete = await _repository.Room.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (roomDelete is not null)
            {
                await DeletePhotosAsync(id);
                _repository.Room.Delete(roomDelete);
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
                throw new RoomNotFoundException(id);
            }
        }

        public async Task DeletePhotosAsync(string roomId)
        {
            var photos = await _repository.Photo.FindCondition(r => r.RoomId!.Equals(roomId))
                .ToListAsync();
            var count = 0;
            foreach (var photo in photos)
            {
                var deleteResult = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (deleteResult.Error != null || deleteResult.Result == "not found")
                {
                    throw new CustomizeException(Failure.DeletePhotoFailing);
                }
                _repository.Photo.Delete(photo);
                count++;
            }
            if (count != 0) await _repository.SaveAsync();
        }

        public async Task<Response> InsertAsync(RoomDto roomDto, IFormFile[]? files)
        {
            await ValidateObject(roomDto);

            var room = _mapper.Map<Room>(roomDto);
            _repository.Room.Create(room);
            await _repository.SaveAsync();

            await InsertPhotosAsync(room.Id, files);

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response> InsertPhotoAsync(string roomId, IFormFile file)
        {
            var room = await _repository.Room.FindCondition(r => r.Id.Equals(roomId))
                .FirstOrDefaultAsync();
            if (room != null)
            {
                var uploadPhotoResult = await _photoService.UploadPhotoAsync(file);
                if (uploadPhotoResult.Error != null)
                {
                    throw new CustomizeException(Failure.UploadPhotoFailing);
                }
                var photo = new Photo
                {
                    RoomId = room.Id,
                    Url = uploadPhotoResult.SecureUrl.AbsoluteUri,
                    PublicId = uploadPhotoResult.PublicId,
                };
                _repository.Photo.Create(photo);
                await _repository.SaveAsync();
                return new Response
                {
                    Success = true,
                    Data = _mapper.Map<PhotoDto>(photo),
                    StatusCode = photo != null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NoContent
                };
            }
            else
            {
                throw new RoomNotFoundException(roomId);
            }
        }
        public async Task<Response> DeletePhotoAsync(string roomId, string photoId)
        {
            var photo = await _repository.Photo.FindCondition(
                p => p.RoomId!.Equals(roomId) && p.Id.Equals(photoId)
            ).FirstOrDefaultAsync();
            if (photo != null)
            {
                var deleteResult = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (deleteResult.Error != null || deleteResult.Result == "not found")
                {
                    throw new CustomizeException(Failure.DeletePhotoFailing);
                }
                _repository.Photo.Delete(photo);
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
                throw new PhotoNotFoundException(roomId);
            }
        }

        public async Task InsertPhotosAsync(string roomId, IFormFile[]? files)
        {
            if (files != null)
            {
                var count = 0;
                foreach (var file in files)
                {
                    var uploadPhotoResult = await _photoService.UploadPhotoAsync(file);
                    if (uploadPhotoResult.Error != null)
                    {
                        throw new CustomizeException(Failure.UploadPhotoFailing);
                    }
                    var photo = new Photo
                    {
                        RoomId = roomId,
                        Url = uploadPhotoResult.SecureUrl.AbsoluteUri,
                        PublicId = uploadPhotoResult.PublicId,
                    };
                    _repository.Photo.Create(photo);
                    count++;
                }
                if (count != 0) await _repository.SaveAsync();
            }
        }

        public async Task<Response> UpdateAsync(string id, RoomUpdateDto roomUpdateDto)
        {
            await ValidateObject(roomUpdateDto);

            var room = await _repository.Room.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (room is not null)
            {
                _mapper.Map(roomUpdateDto, room);
                room.UpdatedAt = DateTime.Now;
                _repository.Room.Update(room);
                await _repository.SaveAsync();
            }
            else
            {
                throw new RoomNotFoundException(id);
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.UpdateSucceed,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public Task ValidateObject<T>(T model)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
