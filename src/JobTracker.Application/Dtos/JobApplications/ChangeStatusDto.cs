
using JobTracker.Domain.Enums;

namespace JobTracker.Application.Dtos.JobApplications;

public sealed record ChangeStatusDto(ApplicationStatus Status);
