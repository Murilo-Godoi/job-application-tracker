using JobTracker.Domain.Entities;

namespace JobTracker.Application.Interfaces;

public interface IApplicationRepository
{
    public Task Add(JobApplication app, CancellationToken ct);
    public Task<JobApplication?> GetById(Guid id, CancellationToken ct);
    public Task<JobApplication?> GetByIdWithFollowUps(Guid id, CancellationToken ct);
    public IQueryable<JobApplication> Query(); 
}
