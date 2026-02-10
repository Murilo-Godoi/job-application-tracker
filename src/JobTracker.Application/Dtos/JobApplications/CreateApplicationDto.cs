namespace JobTracker.Application.Dtos.JobApplications;

public sealed record CreateApplicationDto(
    string CompanyName,
    string RoleTitle,
    string? Source,
    DateTime? AppliedAt,
    decimal? SalaryEstimate,
    string? Notes
);
