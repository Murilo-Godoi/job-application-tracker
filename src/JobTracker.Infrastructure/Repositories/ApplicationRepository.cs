using JobTracker.Application.Dtos.JobApplications;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Repositories;

public sealed class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _db;
    public ApplicationRepository(AppDbContext db) => _db = db;

    public Task Add(JobApplication app, CancellationToken ct)
    {
        _db.Applications.Add(app);
        return Task.CompletedTask;
    }

    public Task<JobApplication?> GetById(Guid id, CancellationToken ct)
        => _db.Applications.FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task<JobApplication?> GetByIdWithFollowUps(Guid id, CancellationToken ct)
        => _db.Applications.Include(x => x.FollowUps).FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<ApplicationListItemDto>> List(ApplicationStatus? status, string? q, CancellationToken ct)
    {
        var query = _db.Applications.AsNoTracking();

        if (status is not null)
            query = query.Where(x => x.Status == status);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToLower();
            query = query.Where(x =>
                x.CompanyName.ToLower().Contains(term) ||
                x.RoleTitle.ToLower().Contains(term) ||
                (x.Notes != null && x.Notes.ToLower().Contains(term)));
        }

        return await query
            .OrderByDescending(x => x.UpdatedAt)
            .Select(x => new ApplicationListItemDto(
                x.Id,
                x.CompanyName,
                x.RoleTitle,
                x.Status,
                x.AppliedAt,
                x.UpdatedAt
            ))
            .ToListAsync(ct);
    }

    public Task<JobApplication?> GetByIdWithDetails(Guid id, CancellationToken ct)
    => _db.Applications
        .Include(x => x.Contacts)
        .Include(x => x.FollowUps)
        .FirstOrDefaultAsync(x => x.Id == id, ct);

}
