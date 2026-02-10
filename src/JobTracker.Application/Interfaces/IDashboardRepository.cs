using JobTracker.Application.Dtos.Dashboard;

namespace JobTracker.Application.Interfaces;

public interface IDashboardRepository
{
    Task<IReadOnlyList<StatusCountDto>> GetStatusCounts(CancellationToken ct);

    Task<IReadOnlyList<UpcomingFollowUpDto>> GetUpcomingFollowUps(DateTime fromUtc, DateTime toUtc, int take, CancellationToken ct);
}
