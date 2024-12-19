using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dto;
using Services.Interfaces;

namespace PizzaOrder.Controllers
{
    //propably useless part
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _service;
        
        public PizzaController(IPizzaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return new JsonResult(await _service.GetAllAsync());
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<JsonResult> GetByID(Guid id)
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
