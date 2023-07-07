using Microsoft.AspNetCore.Mvc;
using Services.Services.ConsumerSerivces;
using Services.Services.ConsumerSerivces.Dtos.RequestDtos;
using Services.Services.ConsumerSerivces.Dtos.ResultDtos;

namespace ExtensionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService _consumerService;

        public ConsumerController(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(List<ConsumerListAllResult>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListAll()
        {
            var result = await _consumerService.ListAll();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] ConsumerInsertRequest model)
        {
            int result = await _consumerService.Insert(model);

            return Created("", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(int id, [FromBody] ConsumerUpdateByIdRequest model)
        {
            await _consumerService.UpdateById(id, model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            await _consumerService.DeleteById(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ConsumerGetByIdResult))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _consumerService.GetById(id);

            return Ok(result);
        }
    }
}
