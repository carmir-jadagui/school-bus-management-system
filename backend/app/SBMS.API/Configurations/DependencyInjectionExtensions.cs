using SBMS.API.Validators;
using SBMS.Application.Services;
using SBMS.Domain.Repositories;
using SBMS.Infrastructure.Persistence.MySQL.Repositories;
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

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITestServices, TestServices>();
            services.AddScoped<IBoyServices, BoyServices>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IBoyRepository, BoyRepository>();

            return services;
        }

        public static IServiceCollection RegisterApplicationValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<BoyModelValidator>();

            return services;
        }
    }
}