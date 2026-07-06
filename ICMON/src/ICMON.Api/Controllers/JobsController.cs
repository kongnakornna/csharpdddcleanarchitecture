using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ICMON.Application.Commands.Jobs.CreateJob;
using ICMON.Application.Commands.Jobs.UpdateJobStatus;
using ICMON.Application.Commands.Jobs.AddJobService;
using ICMON.Application.Commands.Jobs.AddJobPart;
using ICMON.Application.Queries.Jobs.GetJobById;
using ICMON.Application.Queries.Jobs.GetJobList;
using ICMON.Application.Queries.Jobs.GetJobStatusSummary;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Interfaces;

namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/jobs")]
[Authorize]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<JobDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateJobCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetJobByIdQuery { Id = id });
        if (!result.IsSuccess)
            return NotFound(result);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<JobDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList([FromQuery] GetJobListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(typeof(Result<JobDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<JobDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateJobStatusCommand command)
    {
        command.JobId = id;
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return result.Error == "Job not found" ? NotFound(result) : BadRequest(result);
        return Ok(result);
    }

    [HttpPost("{id:guid}/services")]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddService(Guid id, [FromBody] AddJobServiceCommand command)
    {
        command.JobId = id;
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPost("{id:guid}/parts")]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddPart(Guid id, [FromBody] AddJobPartCommand command)
    {
        command.JobId = id;
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}/services/{serviceId:guid}")]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveService(Guid id, Guid serviceId)
    {
        var job = await _mediator.Send(new GetJobByIdQuery { Id = id });
        if (!job.IsSuccess)
            return NotFound(job);
        var command = new Application.Commands.Jobs.RemoveJobService.RemoveJobServiceCommand
        {
            JobId = id,
            ServiceId = serviceId
        };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}/parts/{partId:guid}")]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePart(Guid id, Guid partId)
    {
        var job = await _mediator.Send(new GetJobByIdQuery { Id = id });
        if (!job.IsSuccess)
            return NotFound(job);
        var command = new Application.Commands.Jobs.RemoveJobPart.RemoveJobPartCommand
        {
            JobId = id,
            PartId = partId
        };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return NotFound(result);
        return Ok(result);
    }

    [HttpGet("summary")]
    [ProducesResponseType(typeof(JobStatusSummary), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatusSummary([FromQuery] GetJobStatusSummaryQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
