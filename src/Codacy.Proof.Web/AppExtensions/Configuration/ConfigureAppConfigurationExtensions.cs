using Amazon;

namespace Codacy.Proof.Web.AppExtensions.Configuration;

internal static class ConfigureAppConfigurationExtensions
{
    internal static IServiceCollection AddSecrets(this IServiceCollection services, ConfigurationManager config)
    {
        AddSecret("globals/whitelist", config);
        AddSecret("globals/tokens", config);
        AddSecret("globals/rtg", config);

        return services;
    }

    private static void AddSecret(string secretName, ConfigurationManager config)
    {
        config.AddSecretsManager(region: RegionEndpoint.USEast1, configurator: options =>
        {
            options.SecretFilter = entry => entry.Name.StartsWith(secretName);
            options.KeyGenerator = (_, s) =>
            {
                var key = s.Replace(secretName, string.Empty)
                    .Replace("__", ":");

                if (key.StartsWith(':'))
                    key = key[1..];

                return key;
            };
            options.PollingInterval = TimeSpan.FromMinutes(60);
        });
    }
}
