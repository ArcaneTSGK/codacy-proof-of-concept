namespace Codacy.Proof.Web.AppExtensions.Exceptions;

internal static class ConfigureExceptionExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<LogExceptionHandling>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}
