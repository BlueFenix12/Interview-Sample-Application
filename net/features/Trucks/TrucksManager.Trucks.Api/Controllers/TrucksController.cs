using Ardalis.Result;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrucksManager.Common;
using TrucksManager.Trucks.CQRS.Commands;
using TrucksManager.Trucks.CQRS.Commands.UpdateTruck;
using TrucksManager.Trucks.CQRS.Queries.SingleTruck;
using TrucksManager.Trucks.CQRS.Queries.TrucksList;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Mappers;

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
            ResultStatus.Ok => this.Ok(result.Value),
            ResultStatus.Invalid => this.BadRequest(result.ValidationErrors),
            _ => this.Problem(result.GetResultErrorsFormatted())
        };
        return actionResult;
    }

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Truck>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationError>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new TrucksList.Query(), cancellationToken);
        IActionResult actionResult = result.Status switch
        {
            ResultStatus.Ok => this.Ok(result.Value),
            ResultStatus.Error => this.Problem(result.GetResultErrorsFormatted()),
            _ => this.Problem()
        };
        return actionResult;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Truck))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Truck))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationError>))]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new GetTruck.Query { Id = id }, cancellationToken);
        IActionResult actionResult = result.Status switch
        {
            ResultStatus.Ok => this.Ok(result.Value),
            ResultStatus.NotFound => this.NotFound(),
            ResultStatus.Error => this.Problem(result.GetResultErrorsFormatted()),
            _ => this.Problem()
        };
        return actionResult;
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Truck))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationError>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTruck([FromRoute] Guid id, [FromBody] UpdateTruck.BodyCommand bodyCommand, CancellationToken cancellationToken)
    {
        var command = bodyCommand.MapToUpdateTruckCommand(id);
        var result = await this.mediator.Send(command, cancellationToken);
        IActionResult actionResult = result.Status switch
        {
            ResultStatus.Ok => this.Ok(),
            ResultStatus.NotFound => this.NotFound(),
            ResultStatus.Invalid => this.BadRequest(result.ValidationErrors),
            ResultStatus.Error => this.Problem(result.GetResultErrorsFormatted()),
            _ => this.Problem()
        };
        return actionResult;
    }
}
