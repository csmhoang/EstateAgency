using Core.Dtos;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/maintenanceRequests")]
    [ApiController]
    public class MaintenanceRequestsController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public MaintenanceRequestsController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin yêu cầu bảo trì
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.MaintenanceRequest.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin yêu cầu bảo trì bằng id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.MaintenanceRequest.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Thêm yêu cầu bảo trì
        /// </summary>
        /// <param name="model">Yêu cầu bảo trì</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaintenanceRequestDto model)
        {
            var response = await _service.MaintenanceRequest.InsertAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật yêu cầu bảo trì
        /// </summary>
        /// <param name="id">Id yêu cầu bảo trì</param>
        /// <param name="model">Yêu cầu bảo trì</param>
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] MaintenanceRequestDto model)
        {
            var response = await _service.MaintenanceRequest.UpdateAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa yêu cầu bảo trì
        /// </summary>
        /// <param name="id">Id yêu cầu bảo trì</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.MaintenanceRequest.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
