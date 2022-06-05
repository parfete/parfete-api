using Parfete.Parties;
using Parfete.Parties.Service;
using Serilog;

var builder = WebApplication
    .CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.AddSerilog(new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger());
});

builder = ConfigurePorts(builder);

builder = ConfigureServices(builder);

var app = builder.Build();

app = ConfigureMiddlewares(app);

app.Run();

static WebApplicationBuilder ConfigurePorts(WebApplicationBuilder builder)
{
    // var httpPort = builder.Configuration["http_port"];
    // var httpsPort = builder.Configuration.GetValue<int>("https_port");

    //builder.WebHost.UseUrls($"http://[::]:{httpPort}", $"https://[::]:{httpsPort}");
    return builder;
}

static WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    var seqUrl = configuration["Serilog:Seq:Url"];
    Log.Logger = new LoggerConfiguration()
       .ReadFrom.Configuration(configuration)
       .CreateLogger();

    builder.Host.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddSerilog(Log.Logger);
    });

    builder.Host.UseSerilog();

    Log.Logger.Debug("Application Starting");

    builder.Services.AddHealthChecks();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
    });

    builder.Services.AddParties().AddPartiesService();
    return builder;
}

static WebApplication ConfigureMiddlewares(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = "";
        });
    }

    app.MapHealthChecks("/health");

    app.UseCors(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });

    app.UseHsts();
    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseApiVersioning();

    app.MapControllers();

    return app;
}

public partial class Program { }