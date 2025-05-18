using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core;

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
    public async Task<Response> GetAllAsync(string userId)
    {
        var rooms = await _repository.Room
            .FindCondition(r => r.LandlordId!.Equals(userId))
            .ToListAsync();
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

    public async Task<Response> GetMyRoomsAsync(string tenantId)
    {

        var bookingDetails = await _repository.Booking
            .FindCondition(
                b => b.TenantId!.Equals(tenantId) && b.Lease!.Status == LeaseEnums.StatusLease.Active
            )
            .Include(b => b.BookingDetails!)
            .ThenInclude(bd => bd.Room!)
            .ThenInclude(r => r.Landlord!)
            .SelectMany(b => b.BookingDetails!)
            .ToListAsync();
        return new Response
        {
            Success = true,
            Data = bookingDetails,
            StatusCode = !bookingDetails.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }


    public async Task<Response> HideAsync(string id)
    {
        var roomHide = await _repository.Room.FindCondition(r => r.Id.Equals(id))
            .Include(r => r.LeaseDetails!)
            .ThenInclude(l => l.Lease!)
            .FirstOrDefaultAsync();
        if(roomHide != null) {
            if(roomHide.LeaseDetails.Any(l => l.Lease!.Status == LeaseEnums.StatusLease.Active))
                throw new CustomizeException(Invalidate.HideInvalidate);
            roomHide.Visibility = false;
            roomHide.UpdatedAt = DateTime.Now;
            _repository.Room.Update(roomHide);
            await _repository.SaveAsync();
            return new Response
            {
                Success = true,
                Messages = Successfull.RemoveSucceed,
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }
        else {
            throw new RoomNotFoundException(id);
        }
    }

    public async Task DeletePhotosAsync(string roomId)
    {
        var photos = await _repository.Photo.FindCondition(r => r.RoomId!.Equals(roomId))
            .ToListAsync();
        foreach(var photo in photos) {
            var deleteResult = await _photoService.DeletePhotoAsync(photo.PublicId);
            if(deleteResult.Error != null && deleteResult.Result != "not found") {
                throw new CustomizeException(Failure.DeletePhotoFailing);
            }
        }
    }

    public async Task<Response> InsertAsync(RoomDto roomDto, IFormFile[]? files)
    {
        await ValidateObject(roomDto);

        var room = _mapper.Map<Room>(roomDto);
        room.Photos = await InsertPhotosAsync(files);

        _repository.Room.Create(room);
        await _repository.SaveAsync();

        return new Response
        {
            Success = true,
            Messages = Successfull.InsertSucceed,
            StatusCode = (int)HttpStatusCode.Created
        };
    }

    public async Task<ICollection<Photo>> InsertPhotosAsync(IFormFile[]? files)
    {
        var photos = new List<Photo>();

        if(files == null)
            return photos;

        foreach(var file in files) {
            var uploadPhotoResult = await _photoService.UploadPhotoAsync(file);
            if(uploadPhotoResult.Error != null) {
                throw new CustomizeException(Failure.UploadPhotoFailing);
            }
            var photo = new Photo
            {
                Url = uploadPhotoResult.SecureUrl.AbsoluteUri,
                PublicId = uploadPhotoResult.PublicId,
            };
            photos.Add(photo);
        }

        return photos;
    }

    public async Task<Response> InsertPhotoAsync(string roomId, IFormFile file)
    {
        var room = await _repository.Room.FindCondition(r => r.Id.Equals(roomId))
            .FirstOrDefaultAsync();
        if(room != null) {
            var uploadPhotoResult = await _photoService.UploadPhotoAsync(file);
            if(uploadPhotoResult.Error != null) {
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
        else {
            throw new RoomNotFoundException(roomId);
        }
    }
    public async Task<Response> DeletePhotoAsync(string roomId, string photoId)
    {
        var photo = await _repository.Photo.FindCondition(
            p => p.RoomId!.Equals(roomId) && p.Id.Equals(photoId)
        ).FirstOrDefaultAsync();
        if(photo != null) {
            var deleteResult = await _photoService.DeletePhotoAsync(photo.PublicId);
            if(deleteResult.Error != null || deleteResult.Result == "not found") {
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
        else {
            throw new PhotoNotFoundException(roomId);
        }
    }

    public async Task<Response> UpdateAsync(string id, RoomUpdateDto roomUpdateDto)
    {
        await ValidateObject(roomUpdateDto);

        var room = await _repository.Room.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if(room is not null) {
            _mapper.Map(roomUpdateDto, room);
            room.UpdatedAt = DateTime.Now;
            _repository.Room.Update(room);
            await _repository.SaveAsync();
        }
        else {
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
