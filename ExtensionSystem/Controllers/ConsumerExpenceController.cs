using Microsoft.AspNetCore.Mvc;
using Services.Services.ConsumerExpenseServices;
using Services.Services.ConsumerExpenseServices.Dtos.RequestDtos;
using Services.Services.ConsumerExpenseServices.Dtos.ResultDtos;

namespace ExtensionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerExpenceController : ControllerBase
    {
        private readonly IconsumerExpenceService _consumerExpenceService;

        public ConsumerExpenceController(IconsumerExpenceService consumerExpenceService)
        {
            _consumerExpenceService = consumerExpenceService;
        }

        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(List<ConsumerExpenseListAllResult>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListAll()
        {
            var result = await _consumerExpenceService.ListAll();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] ConsumerExpenseInsertRequest model)
        {
            int result = await _consumerExpenceService.Insert(model);

            return Created("", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(int id, [FromBody] ConsumerExpenseUpdateByIdRequest model)
        {
            await _consumerExpenceService.UpdateById(id, model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            await _consumerExpenceService.DeleteById(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ConsumerExpenseGetByIdResult))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _consumerExpenceService.GetById(id);

            return Ok(result);
        }

        [HttpGet("getTotalCostByConsumerId/{consumerId}")]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> TotalCostByConsumerId(int consumerId)
        {
            int result = await _consumerExpenceService.TotalCostByConsumerId(consumerId);

            return Ok(result);
        }
    }
}
