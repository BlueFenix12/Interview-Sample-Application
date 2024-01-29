using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace TrucksManager.Trucks.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class PingController : ControllerBase
{
    [HttpGet("")]
    public Task<IActionResult> Ping()
    {
        string resultText = "Pong";
        var result = Ok(resultText);
        return Task.FromResult<IActionResult>(result);
    }
}
