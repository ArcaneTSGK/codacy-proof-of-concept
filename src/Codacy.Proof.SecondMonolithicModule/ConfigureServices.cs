using System.Reflection;
using Codacy.Proof.SecondMonolithicModule.Application.Interfaces;
using Codacy.Proof.SecondMonolithicModule.Infrastructure;
using Codacy.Proof.SecondMonolithicModule.Presentation.Endpoints;
using Codacy.Proof.SharedKernel.Behaviors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codacy.Proof.SecondMonolithicModule;

public static class ConfigureServices
{
    public static IServiceCollection AddModule2Services(this IServiceCollection services, ConfigurationManager config, List<Assembly> mediatrAssemblies)
    {
        mediatrAssemblies.Add(typeof(ConfigureServices).Assembly);
        services.AddValidatorsFromAssemblyContaining<IModule2Marker>();
        services.AddScoped<IReadOnlyGameRepository, ReadOnlyGameRepository>();
        return services;
    }

    public static WebApplication MapModule2Endpoints(this WebApplication app)
    {
        app.MapEndpoints();
        return app;
    }
}
