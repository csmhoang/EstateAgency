namespace Api;

[Route("api/v1/notifications")]
[ApiController]
public class NotificationsController : ControllerBase
{
    #region Declaration
    private readonly IServiceManager _service;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public NotificationsController(IServiceManager service) =>
        _service = service;
    #endregion

    #region Method
    /// <summary>
    /// Phản hồi thông báo
    /// </summary>
    /// <param name="id">Id thông báo</param>
    /// <param name="status">Trạng thái</param>
    [HttpPut("response")]
    [Authorize]
    public async Task<IActionResult> ResponseRequest(string id, NotificationEnums.StatusNotification status)
    {
        var response = await _service.Notification.ResponseAsync(id, status);
        return Ok(response);
    }
    #endregion
}
