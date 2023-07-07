using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.ExpenseServices;
using Services.Services.ExpenseServices.Dtos.RequestDtos;
using Services.Services.ExpenseServices.Dtos.ResultDtos;

namespace ExtensionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenceController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenceController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(List<ExpenseListAllResult>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListAll()
        {
            var result = await _expenseService.ListAll();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] ExpenceInsertRequest model)
        {
            int result = await _expenseService.Insert(model);

            return Created("", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(int id, [FromBody] ExpenceUpdateByIdRequest model)
        {
            await _expenseService.UpdateById(id, model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            await _expenseService.DeleteById(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseGetByIdResult))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _expenseService.GetById(id);

            return Ok(result);
        }
    }
}
