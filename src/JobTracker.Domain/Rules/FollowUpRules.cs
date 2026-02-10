using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Rules;

public static class FollowUpRules
{
    public static FollowUpTask? CreateForStatusChange(JobApplication app, DateTime nowUtc)
    {
        return app.Status switch
        {
            ApplicationStatus.Applied => new FollowUpTask
            {
                ApplicationId = app.Id,
                DueAt = nowUtc.AddDays(3),
                Type = FollowUpType.FollowUp,
                Status = FollowUpStatus.Pending
            },
            ApplicationStatus.Interview => new FollowUpTask
            {
                ApplicationId = app.Id,
                DueAt = nowUtc.AddDays(1),
                Type = FollowUpType.ThankYou,
                Status = FollowUpStatus.Pending
            },
            _ => null
        };
    }
}
