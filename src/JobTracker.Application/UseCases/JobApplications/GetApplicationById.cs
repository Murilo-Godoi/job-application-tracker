using JobTracker.Application.Dtos.JobApplications;
using JobTracker.Application.Interfaces;

namespace JobTracker.Application.UseCases.JobApplications;

public sealed class GetApplicationById
{
    private readonly IApplicationRepository _apps;

    public GetApplicationById(IApplicationRepository apps)
        => _apps = apps;

    public async Task<ApplicationDetailsDto> Handle(Guid id, CancellationToken ct)
    {
        var app = await _apps.GetByIdWithDetails(id, ct)
                  ?? throw new KeyNotFoundException("Application not found");

        return new ApplicationDetailsDto(
            app.Id,
            app.CompanyName,
            app.RoleTitle,
            app.Source,
            app.Status,
            app.AppliedAt,
            app.SalaryEstimate,
            app.Notes,
            app.CreatedAt,
            app.UpdatedAt,
            app.Contacts.Select(c =>
                new ContactDto(
                    c.Id,
                    c.Name,
                    c.Title,
                    c.Channel,
                    c.Value,
                    c.Notes
                )).ToList(),
            app.FollowUps
                .OrderBy(f => f.DueAt)
                .Select(f =>
                    new FollowUpDto(
                        f.Id,
                        f.DueAt,
                        f.Type.ToString(),
                        f.Status.ToString(),
                        f.DoneAt,
                        f.Notes
                    )).ToList()
        );
    }
}
