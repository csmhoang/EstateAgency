namespace Api;

[Route("api/v1/token")]
[ApiController]
public class TokenController : ControllerBase
{
    #region Declaration
    private readonly IServiceManager _service;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public TokenController(IServiceManager service) =>
        _service = service;
    #endregion

    #region Method
    /// <summary>
    /// Làm mới token
    /// </summary>
    [HttpPost("refresh")]
    [Authorize]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var response = await _service.Authentication.RefreshToken(tokenDto);
        return Ok(response);
    }
    #endregion
}
