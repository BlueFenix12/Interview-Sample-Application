using System.Net;
using Ardalis.Result;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrucksManager.Common;
using TrucksManager.Trucks.CQRS.Commands;
using TrucksManager.Trucks.CQRS.Queries.Ping;
using TrucksManager.Trucks.Domain;

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

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddTruck.CommandResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationError>))]
    public async Task<IActionResult> AddTruck([FromBody] AddTruck.Command command, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(command, cancellationToken);
        IActionResult actionResult = result.Status switch
        {
            ResultStatus.Invalid => this.BadRequest(result.ValidationErrors),
            ResultStatus.Ok => this.Ok(result.Value),
            _ => this.Problem(result.GetResultErrorsFormatted())
        };

        return actionResult;
    }
}
