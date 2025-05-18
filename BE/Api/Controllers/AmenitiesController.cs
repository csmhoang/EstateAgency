namespace Api;

[Route("api/v1/amenities")]
[ApiController]
public class AmenitiesController : ControllerBase
{
    #region Declaration
    private readonly IServiceManager _service;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public AmenitiesController(IServiceManager service) =>
        _service = service;
    #endregion

    #region Method
    /// <summary>
    /// Lấy tất cả thông tin tiện nghi
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.Amenity.GetAllAsync();
        return Ok(response);
    }

    /// <summary>
    /// Lấy thông tin tiện nghi bằng id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _service.Amenity.GetAsync(id);
        return Ok(response);
    }

    /// <summary>
    /// Thêm tiện nghi
    /// </summary>
    /// <param name="model">Tiện nghi</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AmenityDto model)
    {
        var response = await _service.Amenity.InsertAsync(model);
        return Ok(response);
    }

    /// <summary>
    /// Cập nhật tiện nghi
    /// </summary>
    /// <param name="id">Id tiện nghi</param>
    /// <param name="model">Tiện nghi</param>
    [HttpPut]
    public async Task<IActionResult> Update(string id, [FromBody] AmenityDto model)
    {
        var response = await _service.Amenity.UpdateAsync(id, model);
        return Ok(response);
    }

    /// <summary>
    /// Xóa tiện nghi
    /// </summary>
    /// <param name="id">Id tiện nghi</param>
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _service.Amenity.DeleteAsync(id);
        return Ok(response);
    }
    #endregion
}
