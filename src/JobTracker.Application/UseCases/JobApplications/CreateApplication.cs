using JobTracker.Application.Dtos.JobApplications;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;

namespace JobTracker.Application.UseCases.JobApplications;
public sealed class CreateApplication
{
    private readonly IApplicationRepository _apps;
    private readonly IUnitOfWork _uow;

    public CreateApplication(IApplicationRepository apps, IUnitOfWork uow)
    {
        _apps = apps;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateApplicationDto dto, CancellationToken ct)
    {
        var app = new JobApplication
        {
            CompanyName = dto.CompanyName.Trim(),
            RoleTitle = dto.RoleTitle.Trim(),
            Source = dto.Source,
            AppliedAt = dto.AppliedAt,
            SalaryEstimate = dto.SalaryEstimate,
            Notes = dto.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _apps.Add(app, ct);
        await _uow.SaveChanges(ct);

        return app.Id;
    }
}