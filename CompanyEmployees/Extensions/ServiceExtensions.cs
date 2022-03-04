using CompanyEmployees.OutputFormatters;
using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;
using LogLevel = NLog.LogLevel;

namespace CompanyEmployees.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }

    public static void ConfigureIISIntegration(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options => {});
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        var config = new NLog.Config.LoggingConfiguration();
        var targetFile = new NLog.Targets.FileTarget("logfile") {FileName = "logfile.txt"};
        
        // Add rule
        config.AddRule(LogLevel.Info, LogLevel.Fatal, targetFile);
        // Apply config
        NLog.LogManager.Configuration = config;
        // Add Logger in IOC
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<RepositoryContext>(options => 
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
    }

    public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder) =>
         builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

}