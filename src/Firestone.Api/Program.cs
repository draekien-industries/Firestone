using Firestone.Application;
using Firestone.Infrastructure;
using Serilog;
using Serilog.Debugging;
using DependencyInjection = Firestone.Application.DependencyInjection;

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

Log.Information("Starting Firestone.Api");

try
{
    SelfLog.Enable(Console.WriteLine);

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services.AddWaystoneApiServiceBuilder(
                builder.Environment,
                builder.Configuration,
                typeof(DependencyInjection))
           .AcceptDefaults("Firestone.Api", "v1", "API for managing FIRE progression data.");

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Host.UseWaystoneApiHostBuilder()
           .AcceptDefaults();

    WebApplication app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseOpenApi();
        app.UseSwaggerUi3();
        app.UseReDoc(options => options.Path = "/docs");
    }

    app.UseWaystoneApiApplicationBuilder()
       .AcceptDefaults();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly. Check the WebHost configuration");
}
finally
{
    Log.Information("Firestone.Api stopped");
    Log.CloseAndFlush();
}

/// <summary>Expose Program for integration tests</summary>
public partial class Program
{ }
