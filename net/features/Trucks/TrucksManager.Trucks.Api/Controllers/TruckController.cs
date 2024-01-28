using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace TrucksManager.Trucks.Api.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TruckController : ControllerBase
{
    [HttpGet("ping")]
    public Task<IActionResult> Ping([FromQuery] string text)
    {
        string resultText = $"Received: {text}";
        var result = Ok(resultText);
        return Task.FromResult<IActionResult>(result);
    }
}
