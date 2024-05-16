using Asp.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Codacy.Proof.Web.AppExtensions.OpenApi;

/// <summary>
/// Register API versioning, Open API and Swagger for to self document the API
/// </summary>
/// <remarks>This allows registration of api versions and self-documentation for the swagger endpoint</remarks>
/// <example>https://github.com/dotnet/aspnet-api-versioning/blob/main/examples/AspNetCore/WebApi/MinimalOpenApiExample/Program.cs</example>
internal static class ConfigureApiDocumentationExtensions
{
    public static IServiceCollection AddApiDocumentation(this IServiceCollection services,
        Action<ApiDocumentationOptions> apiVersioningOptions)
    {
        services.AddEndpointsApiExplorer();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
            options.OperationFilter<AddRequiredHeaderParameter>();
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Security Definition",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                In = ParameterLocation.Header
            };
            var requirement = new OpenApiSecurityRequirement
            {
                { scheme, new List<string>() }
            };
            options.AddSecurityRequirement(requirement);
        });

        services.AddApiVersioning(options =>
        {
            // Access the configured options
            var apiDocOptions = new ApiDocumentationOptions();
            apiVersioningOptions(apiDocOptions);

            options.DefaultApiVersion = new ApiVersion(apiDocOptions.DefaultVersion);
            options.AssumeDefaultVersionWhenUnspecified = apiDocOptions.AssumeDefaultVersion;
            options.ApiVersionReader = apiDocOptions.ApiVersionReader;
            options.ReportApiVersions = apiDocOptions.ReportApiVersions;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static WebApplication UseSwaggerEx(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });

        return app;
    }
}
