using JobTracker.Application.Interfaces;
using JobTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("Default");

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(conn));

        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        //services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IFollowUpRepository, FollowUpRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IClock,SystemClock>();

        return services;
    }
}
