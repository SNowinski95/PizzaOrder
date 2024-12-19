using Microsoft.AspNetCore.Mvc;
using Services.Dto;
using Services.Interfaces;

namespace PizzaOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderDto dto)
        {
            await _service.InsertAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return new JsonResult(await _service.GetAllAsync());
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<JsonResult> GetById(Guid id)
        {
            return new JsonResult(await _service.GetByIdAsync(id));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
