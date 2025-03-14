using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var common = new HostBuilder()
                 .ConfigureAppConfiguration((hostContext, config) =>
                 {
                     var env = hostContext.HostingEnvironment.EnvironmentName;
                     config
                        .AddCommandLine(args)
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                 })
                 .ConfigureFunctionsWebApplication()
                 .ConfigureServices(services =>
                 {
                     services.AddApplicationInsightsTelemetryWorkerService();
                     services.ConfigureFunctionsApplicationInsights();
                     //services.Configure<LoggerFilterOptions>(options =>
                     //{
                     //    // The Application Insights SDK adds a default logging filter that instructs ILogger to capture only Warning and more severe logs. Application Insights requires an explicit override.
                     //    // Log levels can also be configured using appsettings.json. For more information, see https://learn.microsoft.com/en-us/azure/azure-monitor/app/worker-service#ilogger-logs
                     //    LoggerFilterRule? toRemove = options.Rules.FirstOrDefault(rule => rule.ProviderName
                     //        == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

                     //    if (toRemove is not null)
                     //    {
                     //        options.Rules.Remove(toRemove);
                     //    }
                     //});
                 })
                 .Build();
common.Run();
