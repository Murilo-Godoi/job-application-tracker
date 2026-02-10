using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
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

    public IQueryable<JobApplication> Query()
        => _db.Applications.AsNoTracking();
}
