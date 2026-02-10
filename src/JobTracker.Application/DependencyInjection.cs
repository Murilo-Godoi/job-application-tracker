using Microsoft.Extensions.DependencyInjection;
using JobTracker.Application.UseCases.JobApplications;
using JobTracker.Application.UseCases.Dashboard;

namespace JobTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Applications
        services.AddScoped<CreateApplication>();
        services.AddScoped<ChangeStatus>();
        services.AddScoped<ListApplications>();
        services.AddScoped<GetApplicationById>();

        // Dashboard
        services.AddScoped<GetDashboardSummary>();

        return services;
    }
}
