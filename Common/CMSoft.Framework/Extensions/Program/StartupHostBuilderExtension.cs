using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace CMSoft.Framework.Extensions.Program
{
    /// <summary>
    /// Rhis is a extension to HostBuilder
    /// </summary>
    public static class StartupHostBuilderExtension
    {
        /// <summary>
        /// Apply combination of "Appsettings.json" according to the environment p.eg appsettings.json (base) + appsettings.production.json (Only applicable to production)
        /// </summary>
        /// <param name="configuration">Current Configuration</param>
        /// <param name="environmentName">Environment name</param>
        /// <param name="configName">Name of file configuration </param>
        /// <returns>HostBuilder <see cref="IHostBuilder"/></returns>
        public static IHostBuilder SetAppSettings(this IHostBuilder configuration, string environmentName, string configName = "appsettings")
        {
            string configName2 = configName;
            string environmentName2 = environmentName;
            return configuration.ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration
                    .AddJsonFile(configName2 + ".json", optional: false, reloadOnChange: false)
                    .AddJsonFile(configName2 + "." + environmentName2 + ".json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables();
            });
        }
    }
}
