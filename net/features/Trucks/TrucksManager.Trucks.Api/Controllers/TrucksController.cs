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
    public async Task<IActionResult> Ping([FromQuery] PingQuery pingQuery, CancellationToken cancellationToken)
    {
        var validationResult = await this.mediator.Send(new ValidationQuery<PingQuery> { Data = pingQuery}, cancellationToken);
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ValidationErrors);
        }
        
        var queryResult = await this.mediator.Send(pingQuery, cancellationToken);
        if(!queryResult.IsSuccess)
        {
            return Problem(queryResult.Value);
        }
        
        return Ok(queryResult.Value);
    }
}
