using JobTracker.Application.Dtos.JobApplications;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;

namespace JobTracker.Application.Interfaces;

public interface IApplicationRepository
{
    public Task Add(JobApplication app, CancellationToken ct);
    public Task<JobApplication?> GetById(Guid id, CancellationToken ct);
    public Task<JobApplication?> GetByIdWithFollowUps(Guid id, CancellationToken ct);
    Task<IReadOnlyList<ApplicationListItemDto>> List(ApplicationStatus? status, string? q, CancellationToken ct);
    Task<JobApplication?> GetByIdWithDetails(Guid id, CancellationToken ct);

}
