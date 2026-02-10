using JobTracker.Application.Interfaces;

namespace JobTracker.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db) => _db = db;

    public Task<int> SaveChanges(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
