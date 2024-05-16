using System.Reflection;
using Codacy.Proof.FirstMonolithicModule.Application.Interfaces;
using Codacy.Proof.FirstMonolithicModule.Infrastructure;
using Codacy.Proof.FirstMonolithicModule.Presentation.Endpoints;
using Codacy.Proof.SharedKernel.Behaviors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codacy.Proof.FirstMonolithicModule;

public static class ConfigureServices
{
    public static IServiceCollection AddFirstMonolithicModuleServices(this IServiceCollection services, ConfigurationManager config, List<Assembly> mediatrAssemblies)
    {
        mediatrAssemblies.Add(typeof(ConfigureServices).Assembly);
        services.AddValidatorsFromAssemblyContaining<IFirstMonolithicModuleMarker>();
        services.AddScoped<IReadOnlyPlayerRepository, ReadOnlyPlayerRepository>();
        return services;
    }

    public static WebApplication MapFirstMonolithicModuleEndpoints(this WebApplication app)
    {
        app.MapEndpoints();
        return app;
    }
}
