using Core.Extensions;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/conversations")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ConversationsController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy ra tất cả hội thoại
        /// </summary>
        [HttpGet("allOfUserCurrent")]
        [Authorize]
        public async Task<IActionResult> GetAllByUserId()
        {
            var userId = User.GetUserId();
            var response = await _service.Conversation.GetAllByUserIdAsync(userId);
            return Ok(response);
        }

        /// <summary>
        /// Lấy ra cuộc hội thoại bằng id của hai người nếu không có tự tạo
        /// </summary>
        [HttpGet("otherId/{otherId}")]
        [Authorize]
        public async Task<IActionResult> GetByOtherId(string otherId)
        {
            var callerId = User.GetUserId();
            var response = await _service.Conversation.GetByTwoUserIdAsync(callerId, otherId);
            return Ok(response);
        }

        #endregion
    }
}
