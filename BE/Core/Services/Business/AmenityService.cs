using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core;

internal class AmenityService : ServiceBase<Amenity>, IAmenityService
{
    #region Declaration
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public AmenityService(IRepositoryManager repository,
               ILoggerManager logger,
               IMapper mapper) : base(repository.Amenity)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    #endregion

    #region Method
    public async Task<Response> GetAllAsync()
    {
        var amenities = await _repository.Amenity.FindAll().ToListAsync();
        return new Response
        {
            Success = true,
            Data = _mapper.Map<IEnumerable<AmenityDto>>(amenities),
            StatusCode = !amenities.Any() ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }

    public async Task<Response> GetAsync(string id)
    {
        var amenity = await _repository.Amenity.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        return new Response
        {
            Success = true,
            Data = _mapper.Map<AmenityDto>(amenity),
            StatusCode = amenity is null ? (int)HttpStatusCode.NoContent : (int)HttpStatusCode.OK
        };
    }
    public async Task<Response> DeleteAsync(string id)
    {
        var amenityDelete = await _repository.Amenity.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (amenityDelete is not null)
        {
            _repository.Amenity.Delete(amenityDelete);
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
            throw new AmenityNotFoundException(id);
        }
    }
    public async Task<Response> InsertAsync(AmenityDto amenityDto)
    {
        await ValidateObject(amenityDto);

        var amenity = _mapper.Map<Amenity>(amenityDto);
        _repository.Amenity.Create(amenity);
        await _repository.SaveAsync();

        return new Response
        {
            Success = true,
            Messages = Successfull.InsertSucceed,
            StatusCode = (int)HttpStatusCode.Created
        };
    }

    public async Task<Response> UpdateAsync(string id, AmenityDto amenityDto)
    {
        await ValidateObject(amenityDto);

        var amenity = await _repository.Amenity.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (amenity is not null)
        {
            _mapper.Map(amenityDto, amenity);
            _repository.Amenity.Update(amenity);
            await _repository.SaveAsync();
        }
        else
        {
            throw new AmenityNotFoundException(id);
        }

        return new Response
        {
            Success = true,
            Messages = Successfull.UpdateSucceed,
            StatusCode = (int)HttpStatusCode.OK
        };
    }
    public Task ValidateObject(AmenityDto amenityDto)
    {
        return Task.CompletedTask;
    }
    #endregion
}
