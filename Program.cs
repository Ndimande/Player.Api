

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Player.Api.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Player.Api;

namespace Player.Api
{
    /// <summary>
    ///     Provides for the application's hosting environment, configuration, and execution.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>
        ///     Provides the application's main entry point.
        /// </summary>
        /// <param name="args">A <see cref="string" />[] representing the application's arguments.</param>
        public static void Main(string[] args)
        {
            /*
             * CRITICAL: APIs must use the invariant culture.
             */
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            var host = CreateHostBuilder(args).Build();
            MigrateDatabase(host);
            host.Run();
        }

        /// <summary>
        ///     Builds the application's host.
        /// </summary>
        /// <param name="args">A <see cref="string" />[] representing the application's arguments.</param>
        /// <returns>An <see cref="IHostBuilder" /> representing the host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    var loglevel = hostingContext.Configuration.GetSection("Logging:Debug:LogLevel:Default");
                    logging.SetMinimumLevel(GetLogLevel(loglevel.Value));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions => { }).UseStartup<Startup>();
                });
        }

        /// <summary>
        ///     Applies the database migrations to the database.
        /// </summary>
        /// <param name="host"> A program abstraction.</param>
        private static void MigrateDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PlayerDBContext>();
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

            }
        }

        /// <summary>
        ///     Translates a <see cref="LogLevel" /> to a <see cref="string" />.
        /// </summary>
        /// <param name="logLevel">
        ///     A <see cref="string" /> representing the log level.
        /// </param>
        /// <returns>
        ///     A <see cref="LogLevel" /> representing the log level to use.
        /// </returns>
        private static LogLevel GetLogLevel(string logLevel)
        {
            return logLevel switch
            {
                "Trace" => LogLevel.Trace,
                "Debug" => LogLevel.Debug,
                "Information" => LogLevel.Information,
                "Warning" => LogLevel.Warning,
                "Error" => LogLevel.Error,
                "Critical" => LogLevel.Critical,
                "None" => LogLevel.None,
                _ => LogLevel.Information
            };
        }
    }
}