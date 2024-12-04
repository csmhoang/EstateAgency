using Core.Dtos;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Core.Enums.BookingEnums;
using static Core.Enums.InvoiceEnums;

namespace Api.Controllers
{
    [Route("api/v1/invoices")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public InvoicesController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin hóa đơn
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Invoice.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin hóa đơn bằng id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Invoice.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Phản hồi hóa đơn
        /// </summary>
        /// <param name="id">Id hóa đơn</param>
        /// <param name="status">Trạng thái</param>
        [HttpPut("response")]
        [Authorize]
        public async Task<IActionResult> ResponseRequest(string id, StatusInvoice status)
        {
            var response = await _service.Invoice.ResponseAsync(id, status);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật hóa đơn
        /// </summary>
        /// <param name="id">Id hóa đơn</param>
        /// <param name="model">Hóa đơn</param>
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] InvoiceDto model)
        {
            var response = await _service.Invoice.UpdateAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa hóa đơn
        /// </summary>
        /// <param name="id">Id hóa đơn</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Invoice.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
