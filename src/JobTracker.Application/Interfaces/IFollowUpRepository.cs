using JobTracker.Domain.Entities;

namespace JobTracker.Application.Interfaces;

public interface IFollowUpRepository
{
    Task Add(FollowUpTask task, CancellationToken ct);
}