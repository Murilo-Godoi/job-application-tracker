using JobTracker.Application.Dtos.JobApplications;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Enums;

namespace JobTracker.Application.UseCases.JobApplications;

public sealed class ListApplications
{
    private readonly IApplicationRepository _apps;

    public ListApplications(IApplicationRepository apps)
        => _apps = apps;

    public Task<IReadOnlyList<ApplicationListItemDto>> Handle(ApplicationStatus? status, string? q, CancellationToken ct)
        => _apps.List(status, q, ct);
}
