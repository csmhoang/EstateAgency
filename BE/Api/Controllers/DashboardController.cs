namespace Api.Controllers;

[Route("api/v1/dashboards")]
[ApiController]
public class DashboardsController : ControllerBase
{
    #region Declaration
    private readonly IServiceManager _service;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public DashboardsController(IServiceManager service) =>
        _service = service;
    #endregion

    #region Method
    /// <summary>
    /// Số phòng người dùng sở hữu
    /// </summary>
    [HttpGet("room-count")]
    [Authorize]
    public async Task<IActionResult> RoomCount()
    {
        var userId = User.GetUserId();
        var response = await _service.Dashboard.RoomCountAsync(userId);
        return Ok(response);
    }
    /// <summary>
    /// Số phòng trống
    /// </summary>
    /// <returns></returns>
    [HttpGet("room-blank-count")]
    [Authorize]
    public async Task<IActionResult> RoomBlankCount()
    {
        var userId = User.GetUserId();
        var response = await _service.Dashboard.RoomBlankCountAsync(userId);
        return Ok(response);
    }
    /// <summary>
    /// Số người thuê trọ theo mốc thời gian
    /// </summary>
    [HttpGet("tenant-count")]
    [Authorize]
    public async Task<IActionResult> TenantCount()
    {
        var userId = User.GetUserId();
        var response = await _service.Dashboard.TenantCountAsync(userId);
        return Ok(response);
    }

    /// <summary>
    /// Doanh thu hàng năm
    /// </summary>
    [HttpGet("revenue")]
    [Authorize]
    public async Task<IActionResult> Revenue()
    {
        var userId = User.GetUserId();
        var response = await _service.Dashboard.RevenueAsync(userId);
        return Ok(response);
    }

    /// <summary>
    /// Số lượt truy cập
    /// </summary>
    [HttpGet("visit-count")]
    [Authorize]
    public async Task<IActionResult> VisitCount(int numberOfMonth)
    {
        var response = await _service.Dashboard.VisitCountAsync(numberOfMonth);
        return Ok(response);
    }
    #endregion
}
