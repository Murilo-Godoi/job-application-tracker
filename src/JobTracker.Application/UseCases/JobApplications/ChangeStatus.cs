using JobTracker.Application.Interfaces;
using JobTracker.Domain.Enums;
using JobTracker.Domain.Rules;

namespace JobTracker.Application.UseCases.JobApplications;

public sealed class ChangeStatus
{
    private readonly IApplicationRepository _apps;
    private readonly IFollowUpRepository _followUps;
    private readonly IUnitOfWork _uow;

    public ChangeStatus(IApplicationRepository apps, IFollowUpRepository followUps, IUnitOfWork uow)
    {
        _apps = apps;
        _followUps = followUps;
        _uow = uow;
    }

    public async Task Handle(Guid appId, ApplicationStatus newStatus, CancellationToken ct)
    {
        var app = await _apps.GetByIdWithFollowUps(appId, ct)
                  ?? throw new KeyNotFoundException("Application not found");

        app.Status = newStatus;
        app.UpdatedAt = DateTime.UtcNow;

        var suggested = FollowUpRules.CreateForStatusChange(app, DateTime.UtcNow);
        if (suggested is not null)
        {
            var exists = app.FollowUps.Any(f => f.Status == FollowUpStatus.Pending && f.Type == suggested.Type);
            if (!exists)
                await _followUps.Add(suggested, ct);
        }

        await _uow.SaveChanges(ct);
    }
}
