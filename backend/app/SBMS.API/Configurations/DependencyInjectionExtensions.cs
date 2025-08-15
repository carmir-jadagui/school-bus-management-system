using Serilog;
using System.Reflection;

namespace SBMS.API.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection LoggerConfigurations(this IServiceCollection services)
        {
            var logFile = AppSettingsExtensions.Configuration.GetSection("LogFiles").Value;

            var log = new LoggerConfiguration()
                .WriteTo.File(logFile.Replace("{date}", DateTime.Today.ToString("yyyy-MM-dd")), shared: true)
                .MinimumLevel.Error()
                .CreateLogger();

            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("SBMS.Infrastructure.Persistence.MySQL"));

            return services;
        }
    }
}