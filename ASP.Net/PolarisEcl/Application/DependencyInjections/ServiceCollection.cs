
using Microsoft.Extensions.DependencyInjection;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Application.Services;

namespace PolarisEcl.Application.DependencyInjections;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAllApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IDashboardSerivce, DashboardService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<ISettingsService, SettingServices>();
        services.AddScoped<IEclAnalysisService, EclAnalysisService>();

        return services;
    }
}