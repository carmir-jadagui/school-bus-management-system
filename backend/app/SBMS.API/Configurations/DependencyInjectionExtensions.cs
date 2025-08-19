using SBMS.API.Validators;
using SBMS.Application.Services;
using SBMS.Domain.Repositories;
using SBMS.Persistence.MySQL.Repositories;
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
            services.AddAutoMapper(Assembly.Load("SBMS.Persistence.MySQL"));

            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITestServices, TestServices>();
            services.AddScoped<IBoyServices, BoyServices>();
            services.AddScoped<IBusServices, BusServices>();
            services.AddScoped<IDriverServices, DriverServices>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IPersonBaseRepository<BoyModel>, BoyRepository>();
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<IPersonBaseRepository<DriverModel>, DriverRepository>();

            return services;
        }

        public static IServiceCollection RegisterApplicationValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<BoyModelValidator>();
            services.AddValidatorsFromAssemblyContaining<BusModelValidator>();
            services.AddValidatorsFromAssemblyContaining<DriverModelValidator>();

            return services;
        }
    }
}