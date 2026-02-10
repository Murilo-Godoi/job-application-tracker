
using JobTracker.Domain.Enums;

namespace JobTracker.Application.Dtos.JobApplications;

public sealed record ApplicationDetailsDto(
    Guid Id,
    string CompanyName,
    string RoleTitle,
    string? Source,
    ApplicationStatus Status,
    DateTime? AppliedAt,
    decimal? SalaryEstimate,
    string? Notes,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IReadOnlyList<ContactDto> Contacts,
    IReadOnlyList<FollowUpDto> FollowUps
);

public sealed record ContactDto(
    Guid Id,
    string Name,
    string? Title,
    ContactChannel? Channel,
    string? Value,
    string? Notes
);

public sealed record FollowUpDto(
    Guid Id,
    DateTime DueAt,
    string Type,
    string Status,
    DateTime? DoneAt,
    string? Notes
);
