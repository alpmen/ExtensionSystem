using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.ConsumerSerivces;
using Services.Services.ConsumerSerivces.Dtos.ResultDtos;
using Services.Services.TokenServices;

namespace ExtensionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConsumerService _consumerService;

        public AuthController(ITokenService tokenService, IConsumerService consumerService)
        {
            _tokenService = tokenService;
            _consumerService = consumerService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string name, [FromQuery] string password)
        {
            var consumer = await _consumerService.GetByNameandPassword(name, password);

            if (consumer)
            {
                var token = _tokenService.CreateToken();

                return Created("", token);
            }

            return BadRequest("user name or password wrong!");
        }
    }
}
