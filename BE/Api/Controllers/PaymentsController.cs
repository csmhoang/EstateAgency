namespace Api.Controllers;

[Route("api/v1/payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    #region Declaration
    private readonly IServiceManager _service;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public PaymentsController(IServiceManager service) =>
        _service = service;
    #endregion

    #region Method
    /// <summary>
    /// Lấy tất cả thông tin thanh toán
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.Payment.GetAllAsync();
        return Ok(response);
    }

    /// <summary>
    /// Lấy thông tin thanh toán bằng id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _service.Payment.GetAsync(id);
        return Ok(response);
    }

    /// <summary>
    /// Thêm thanh toán
    /// </summary>
    /// <param name="invoiceId">Id hóa đơn</param>
    [HttpPost]
    public async Task<IActionResult> Create(string invoiceId)
    {
        var response = await _service.Payment.InsertAsync(invoiceId);
        return Ok(response);
    }

    /// <summary>
    /// Cập nhật thanh toán
    /// </summary>
    /// <param name="id">Id thanh toán</param>
    /// <param name="model">Thanh toán</param>
    [HttpPut]
    public async Task<IActionResult> Update(string id, [FromBody] PaymentDto model)
    {
        var response = await _service.Payment.UpdateAsync(id, model);
        return Ok(response);
    }

    /// <summary>
    /// Xóa thanh toán
    /// </summary>
    /// <param name="id">Id thanh toán</param>
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _service.Payment.DeleteAsync(id);
        return Ok(response);
    }
    #endregion
}
