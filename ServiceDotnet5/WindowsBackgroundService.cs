using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceDotnet5
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly Service _service;
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly AppConfigurations _configurations;

        public WindowsBackgroundService(Service service, ILogger<WindowsBackgroundService> logger, AppConfigurations configurations)  // => (_service, _logger, _configurations) = (service, logger, configurations);
        {
            _service = service;
            _logger = logger;
            _configurations = configurations;
        }        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {            
            _logger.LogInformation($"Application {_configurations.ServiceName} started!");

            while (!stoppingToken.IsCancellationRequested)
            {
                _service.Main();
                await Task.Delay(TimeSpan.FromSeconds(int.Parse(_configurations.Polling)), stoppingToken);
            }
        }
    }
}
