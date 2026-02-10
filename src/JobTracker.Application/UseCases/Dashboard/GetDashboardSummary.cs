using JobTracker.Application.Dtos.Dashboard;
using JobTracker.Application.Interfaces;

namespace JobTracker.Application.UseCases.Dashboard;
public sealed class GetDashboardSummary
{
    private readonly IDashboardRepository _repo;

    public GetDashboardSummary(IDashboardRepository repo) => _repo = repo;

    public async Task<DashboardSummaryDto> Handle(DateTime nowUtc, CancellationToken ct)
    {
        // janelas em UTC (simples e previsível)
        var startToday = new DateTime(nowUtc.Year, nowUtc.Month, nowUtc.Day, 0, 0, 0, DateTimeKind.Utc);
        var startTomorrow = startToday.AddDays(1);
        var startDayAfterTomorrow = startTomorrow.AddDays(1);
        var startNext7 = startToday.AddDays(7);

        var counts = await _repo.GetStatusCounts(ct);

        // limites pra não virar endpoint pesado
        var dueToday = await _repo.GetUpcomingFollowUps(startToday, startTomorrow, take: 20, ct);
        var dueTomorrow = await _repo.GetUpcomingFollowUps(startTomorrow, startDayAfterTomorrow, take: 20, ct);
        var next7Days = await _repo.GetUpcomingFollowUps(startToday, startNext7, take: 50, ct);

        return new DashboardSummaryDto(counts, dueToday, dueTomorrow, next7Days);
    }
}

