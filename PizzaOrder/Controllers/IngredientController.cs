using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dto;
using Services.Interfaces;

namespace PizzaOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;
        public IngredientController(IIngredientService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Post(IngredientDto model)
        {
            try
            {
                await _service.InsertAsync(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(IngredientDto model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            return new JsonResult(await _service.GetAllAsync());
        }
    }
}
