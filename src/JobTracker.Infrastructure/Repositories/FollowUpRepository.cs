using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;

namespace JobTracker.Infrastructure.Repositories;

public sealed class FollowUpRepository : IFollowUpRepository
{
    private readonly AppDbContext _db;
    public FollowUpRepository(AppDbContext db) => _db = db;

    public Task Add(FollowUpTask task, CancellationToken ct)
    {
        _db.FollowUps.Add(task);
        return Task.CompletedTask;
    }
}