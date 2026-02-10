using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Entities;

public sealed class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ApplicationId { get; set; }
    public JobApplication Application { get; set; } = default!;

    public string Name { get; set; } = default!;
    public string? Title { get; set; }
    public ContactChannel? Channel { get; set; }
    public string? Value { get; set; }
    public string? Notes { get; set; }
}
