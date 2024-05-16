using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Codacy.Proof.SharedKernel.Behaviors;

public static class BehaviorExtensions
{
    public static IServiceCollection AddMediatrLoggingBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }

    public static IServiceCollection AddMediatrValidationBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }

    public static IServiceCollection AddValidatorsFromAssemblyContaining<T>(
        this IServiceCollection services)
    {
        // Get the assembly containing the specified type
        var assembly = typeof(T).GetTypeInfo().Assembly;

        // Find all validator types in the assembly
        var validatorTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IValidator<>)))
            .ToList();

        // Register each validator with its implemented interfaces
        foreach (var validatorType in validatorTypes)
        {
            var implementedInterfaces = validatorType.GetInterfaces()
                .Where(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IValidator<>));

            foreach (var implementedInterface in implementedInterfaces)
            {
                services.AddTransient(implementedInterface, validatorType);
            }
        }

        return services;
    }
}
