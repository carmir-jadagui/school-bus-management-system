using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SBMS.Infrastructure.Persistence.MySQL.Entities;

namespace SBMS.API.Configurations
{
    public static class DataBaseExtensions
    {
        public static IServiceCollection RegistrerDataBase(this IServiceCollection services)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = AppSettingsExtensions.GetConfigurationValue("ConnectionString:Server"),
                Database = AppSettingsExtensions.GetConfigurationValue("ConnectionString:DataBase"),
                UserID = AppSettingsExtensions.GetConfigurationValue("ConnectionString:User"),
                Password = AppSettingsExtensions.GetConfigurationValue("ConnectionString:Password"),
                TreatTinyAsBoolean = true
            };

            services.AddDbContext<SBMSContext>(conf =>
                conf.UseMySql(builder.ConnectionString,                 // cadena de conexión
                ServerVersion.AutoDetect(builder.ConnectionString)));   // detecta versión de MySQL));

            return services;
        }
    }
}