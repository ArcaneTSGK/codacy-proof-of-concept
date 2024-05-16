using System.Reflection;
using Codacy.Proof.FirstMonolithicModule;
using Codacy.Proof.SecondMonolithicModule;
using Codacy.Proof.SharedKernel.Behaviors;
using Codacy.Proof.Web.AppExtensions.Configuration;
using Codacy.Proof.Web.AppExtensions.Exceptions;
using Codacy.Proof.Web.AppExtensions.OpenApi;
using Destructurama;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var logger = Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .Enrich.FromLogContext()
    .Destructure.UsingAttributes()
    .CreateLogger();

builder.Host.UseSerilog((_, cfg) =>
    cfg.ReadFrom.Configuration(config)
        .Destructure.UsingAttributes());

builder.Services.AddSingleton(logger);

logger.Information("Initializing web host");

builder.Services.AddExceptionHandling();

builder.Services.AddSecrets(config);


builder.Services.AddApiDocumentation(_ => { });

List<Assembly> mediatrAssemblies = [typeof(Codacy.Proof.Web.Program).Assembly];
builder.Services.AddFirstMonolithicModuleServices(config, mediatrAssemblies);
builder.Services.AddModule2Services(config, mediatrAssemblies);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(mediatrAssemblies.ToArray()));

builder.Services.AddMediatrLoggingBehavior();

builder.Services.AddMediatrValidationBehavior();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionHandler(_ => { });

app.MapFirstMonolithicModuleEndpoints();
app.MapModule2Endpoints();

app.UseSwaggerEx();

app.Run();

namespace Codacy.Proof.Web
{
    public partial class Program;
}
