using JobTracker.Application.Dtos.JobApplications;
using JobTracker.Application.UseCases.JobApplications;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("job-applications")]
public sealed class ApplicationsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateApplicationDto dto,
        [FromServices] CreateApplication uc,
        CancellationToken ct)
    {
        var id = await uc.Handle(dto, ct);
        return CreatedAtAction(nameof(GetByIdPlaceholder), new { id }, new { id });
    }

    // TODO: organizar rotas em arquivo separado
    [HttpGet("{id:guid}")]
    public IActionResult GetByIdPlaceholder(Guid id) => Ok(new { id });

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid id,
        [FromBody] ChangeStatusDto dto,
        [FromServices] ChangeStatus uc,
        CancellationToken ct)
    {
        await uc.Handle(id, dto.Status, ct);
        return NoContent();
    }
}
