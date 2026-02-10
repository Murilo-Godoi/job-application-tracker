using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Entities;

public sealed class FollowUpTask
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ApplicationId { get; set; }
    public JobApplication Application { get; set; } = default!;

    public DateTime DueAt { get; set; }
    public FollowUpType Type { get; set; } = FollowUpType.FollowUp;
    public FollowUpStatus Status { get; set; } = FollowUpStatus.Pending;

    public DateTime? DoneAt { get; set; }
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
