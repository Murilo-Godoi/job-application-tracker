using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Dtos.Dashboard;

public sealed record DashboardSummaryDto(
    IReadOnlyList<StatusCountDto> Counts,
    IReadOnlyList<UpcomingFollowUpDto> DueToday,
    IReadOnlyList<UpcomingFollowUpDto> DueTomorrow,
    IReadOnlyList<UpcomingFollowUpDto> Next7Days
);

public sealed record StatusCountDto(string Status, int Count);

public sealed record UpcomingFollowUpDto(
    Guid Id,
    DateTime DueAt,
    string Type,
    Guid ApplicationId,
    string CompanyName,
    string RoleTitle
);

