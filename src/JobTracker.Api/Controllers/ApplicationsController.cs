using JobTracker.Application.Dtos.JobApplications;
using JobTracker.Application.UseCases.JobApplications;
using JobTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Api.Controllers;

[ApiController]
[Route("job-applications")]
public sealed class ApplicationsController : ControllerBase
{
    // TODO: organizar rotas em arquivo separado
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApplicationDto dto, [FromServices] CreateApplication uc, CancellationToken ct)
    {
        var id = await uc.Handle(dto, ct);
        return CreatedAtAction(nameof(CreateApplication), new { id }, new { id });
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeStatusDto dto, [FromServices] ChangeStatus uc, CancellationToken ct)
    {
        await uc.Handle(id, dto.Status, ct);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ApplicationStatus? status, [FromQuery] string? q, [FromServices] ListApplications uc, CancellationToken ct)
    {
        var result = await uc.Handle(status, q, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromServices] GetApplicationById uc, CancellationToken ct)
    {
        var result = await uc.Handle(id, ct);
        return Ok(result);
    }

}
