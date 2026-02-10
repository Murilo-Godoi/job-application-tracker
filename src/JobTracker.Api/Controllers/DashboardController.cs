using JobTracker.Application.UseCases.Dashboard;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("dashboard")]
public sealed class DashboardController : ControllerBase
{
    [HttpGet("summary")]
    public async Task<IActionResult> Summary(
        [FromServices] GetDashboardSummary uc,
        CancellationToken ct)
    {
        var result = await uc.Handle(DateTime.UtcNow, ct);
        return Ok(result);
    }
}
