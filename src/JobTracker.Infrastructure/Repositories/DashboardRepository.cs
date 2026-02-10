using JobTracker.Application.Dtos.Dashboard;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Repositories;

public sealed class DashboardRepository : IDashboardRepository
{
    private readonly AppDbContext _db;
    public DashboardRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<StatusCountDto>> GetStatusCounts(CancellationToken ct)
    {
        var raw = await _db.Applications.AsNoTracking()
            .GroupBy(x => x.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync(ct);

        return raw
            .OrderBy(x => (int)x.Status)
            .Select(x => new StatusCountDto(x.Status.ToString(), x.Count))
            .ToList();
    }

    public async Task<IReadOnlyList<UpcomingFollowUpDto>> GetUpcomingFollowUps(
        DateTime fromUtc,
        DateTime toUtc,
        int take,
        CancellationToken ct)
    {
        return await _db.FollowUps.AsNoTracking()
            .Where(f =>
                f.Status == FollowUpStatus.Pending &&
                f.DueAt >= fromUtc &&
                f.DueAt < toUtc)
            .OrderBy(f => f.DueAt)
            .Include(f => f.Application)
            .Select(f => new UpcomingFollowUpDto(
                f.Id,
                f.DueAt,
                f.Type.ToString(),
                f.ApplicationId,
                f.Application.CompanyName,
                f.Application.RoleTitle
            ))
            .Take(take)
            .ToListAsync(ct);
    }
}
