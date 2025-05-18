namespace Api;

[Route("api/v1/rooms")]
[ApiController]
public class RoomsController : ControllerBase
{
    #region Declaration
    private readonly IServiceManager _service;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public RoomsController(IServiceManager service) =>
        _service = service;
    #endregion

    #region Method
    /// <summary>
    /// Lấy tất cả thông tin phòng
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.GetUserId();
        var response = await _service.Room.GetAllAsync(userId);
        return Ok(response);
    }

    /// <summary>
    /// Lấy danh sách thông tin phòng bằng specification
    /// </summary>
    [HttpGet("list")]
    [Authorize]
    public async Task<IActionResult> GetList([FromQuery] RoomSpecParams specParams)
    {
        var response = await _service.Room.GetListAsync(specParams);
        return Ok(response);
    }

    /// <summary>
    /// Lấy thông tin phòng bằng id
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _service.Room.GetAsync(id);
        return Ok(response);
    }

    /// <summary>
    /// Thêm phòng
    /// </summary>
    [HttpPost]
    [Authorize(Roles = RoleConst.Landlord)]
    public async Task<IActionResult> Create([FromForm] RoomDto model, IFormFile[]? files)
    {
        var response = await _service.Room.InsertAsync(model, files);
        return Ok(response);
    }

    /// <summary>
    /// Thêm ảnh cho phòng
    /// </summary>
    [HttpPost("insert-photo/{roomId}")]
    [Authorize(Roles = RoleConst.Landlord)]
    public async Task<IActionResult> InsertPhoto(string roomId, IFormFile file)
    {
        var response = await _service.Room.InsertPhotoAsync(roomId, file);
        return Ok(response);
    }

    /// <summary>
    /// Xóa ảnh phòng
    /// </summary>
    [HttpDelete("delete-photo")]
    [Authorize(Roles = RoleConst.Landlord)]
    public async Task<IActionResult> DeletePhoto(string roomId, string photoId)
    {
        var response = await _service.Room.DeletePhotoAsync(roomId, photoId);
        return Ok(response);
    }

    /// <summary>
    /// Cập nhật phòng
    /// </summary>
    /// <param name="id">Id phòng</param>
    /// <param name="model">Phòng</param>
    [HttpPut]
    [Authorize(Roles = RoleConst.Landlord)]
    public async Task<IActionResult> Update(string id, [FromBody] RoomUpdateDto model)
    {
        var response = await _service.Room.UpdateAsync(id, model);
        return Ok(response);
    }

    /// <summary>
    /// Ẩn phòng
    /// </summary>
    /// <param name="id">Id phòng</param>
    [HttpDelete("hide")]
    [Authorize(Roles = RoleConst.Landlord)]
    public async Task<IActionResult> Hide(string id)
    {
        var response = await _service.Room.HideAsync(id);
        return Ok(response);
    }
    #endregion
}
