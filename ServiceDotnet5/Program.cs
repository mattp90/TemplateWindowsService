using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using System;
using System.IO;

namespace ServiceDotnet5
{
    class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            
            var ConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            AppConfigurations Configurations = ConfigurationRoot.GetSection("Configurations").Get<AppConfigurations>();

            var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = Configurations.ServiceName;
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<WindowsBackgroundService>();
                    services.AddLogging(loggingBuilder => {
                        loggingBuilder.AddNLog("nlog.config");
                    });
                    services.AddTransient<AppConfigurations>();
                    services.AddTransient<Service>();

                    services.AddSingleton(Configurations);
                })

                .Build();

            host.Run();
        }
    }
}

