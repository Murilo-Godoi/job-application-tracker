
using JobTracker.Domain.Enums;

namespace JobTracker.Application.Dtos.JobApplications;

public sealed record ApplicationListItemDto(
    Guid Id,
    string CompanyName,
    string RoleTitle,
    ApplicationStatus Status,
    DateTime? AppliedAt,
    DateTime UpdatedAt
);
