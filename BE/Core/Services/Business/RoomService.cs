using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
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
        public async Task<Response> DeleteAsync(string id)
        {
            var roomDelete = await _repository.Room.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (roomDelete is not null)
            {
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
        public async Task<Response> InsertAsync(RoomDto roomDto, IFormFile[]? files)
        {
            await ValidateObject(roomDto);

            var room = _mapper.Map<Room>(roomDto);
            _repository.Room.Create(room);
            await _repository.SaveAsync();

            if (files != null)
            {
                var count = 0;
                foreach (var file in files)
                {
                    await InsertPhoto(room.Id, file);
                    count++;
                }
                if (count != 0) await _repository.SaveAsync();
            }

            return new Response
            {
                Success = true,
                Messages = Successfull.InsertSucceed,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task InsertPhoto(string roomId, IFormFile file)
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
        }

        public async Task<Response> UpdateAsync(string id, RoomDto roomDto)
        {
            await ValidateObject(roomDto);

            var room = await _repository.Room.FindCondition(r => r.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (room is not null)
            {
                _mapper.Map(roomDto, room);
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
        public Task ValidateObject(RoomDto roomDto)
        {
            return Task.CompletedTask;
        }

        public async Task<Response> GetListAsync(RoomSpecParams specParams)
        {
            var spec = new RoomSpecification(specParams);
            var page = await CreatePagedResult(spec, specParams.PageIndex, specParams.PageSize);
            return new Response
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<RoomDto>>(page),
                StatusCode = !page.Data.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
            };
        }
        #endregion
    }
}
