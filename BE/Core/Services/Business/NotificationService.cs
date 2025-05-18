using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Core.Enums.NotificationEnums;

namespace Core.Services.Business;

internal class NotificationService : ServiceBase<Notification>, INotificationService
{
    #region Declaration
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public NotificationService(IRepositoryManager repository,
               ILoggerManager logger,
               IMapper mapper) : base(repository.Notification)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    #endregion

    #region Method
    public async Task<Response> ResponseAsync(string id, StatusNotification status)
    {
        var notification = await _repository.Notification.FindCondition(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (notification == null) throw new NotificationNotFoundException(id);
        notification.Status = status;
        notification.UpdatedAt = DateTime.Now;
        _repository.Notification.Update(notification);
        await _repository.SaveAsync();
        return new Response
        {
            Success = true,
            Messages = Successfull.ResponseSucceed,
            StatusCode = (int)HttpStatusCode.NoContent
        };
    }
    #endregion
}
