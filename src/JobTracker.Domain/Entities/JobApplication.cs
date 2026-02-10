using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Entities;


public sealed class JobApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CompanyName { get; set; } = default!;
    public string RoleTitle { get; set; } = default!;
    public string? Source { get; set; }
    public DateTime? AppliedAt { get; set; }

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
    public decimal? SalaryEstimate { get; set; }
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<Contact> Contacts { get; set; } = [];
    public List<FollowUpTask> FollowUps { get; set; } = [];
}
