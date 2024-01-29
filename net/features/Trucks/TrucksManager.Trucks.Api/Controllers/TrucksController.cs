using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrucksManager.Trucks.CQRS.Queries.Ping;

namespace TrucksManager.Trucks.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class TrucksController : ControllerBase
{
    private readonly IMediator mediator;

    public TrucksController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("ping")]
    public async Task<IActionResult> Ping([FromQuery] string text, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new PingQuery { Value = text }, cancellationToken);
        return Ok(result);
    }
}
