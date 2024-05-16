using Asp.Versioning;

namespace Codacy.Proof.Web.AppExtensions.OpenApi;

internal class ApiDocumentationOptions
{
    public double DefaultVersion { get; set; } = 1;
    public bool AssumeDefaultVersion { get; set; } = true;
    public IApiVersionReader ApiVersionReader { get; set; } = new UrlSegmentApiVersionReader();
    public bool ReportApiVersions { get; set; } = true;
}
