using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrucksManager.Common;
using TrucksManager.Trucks.CQRS.Queries.Ping;

namespace TrucksManager.Trucks.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class PingController : ControllerBase
{
    private readonly IMediator mediator;

    public PingController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("")]
    public async Task<IActionResult> Ping([FromQuery] Ping.Query query, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(query, cancellationToken);
        if(result.HasValidationErrors())
        {
            return BadRequest(result.ValidationErrors);
        }

        return result.IsSuccess 
            ? Ok(result.Value) 
            : Problem();
    }
}
