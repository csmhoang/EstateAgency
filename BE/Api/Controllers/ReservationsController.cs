﻿using Core.Dtos;
using Core.Interfaces.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        #region Declaration
        private readonly IServiceManager _service;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ReservationsController(IServiceManager service) =>
            _service = service;
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả thông tin đặt lịch
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.Reservation.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin đặt lịch bằng id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Reservation.GetAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Thêm đặt lịch
        /// </summary>
        /// <param name="model">Đặt lịch</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationDto model)
        {
            var response = await _service.Reservation.InsertAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <param name="model">Đặt lịch</param>
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] ReservationDto model)
        {
            var response = await _service.Reservation.UpdateAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// Xóa đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Reservation.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}