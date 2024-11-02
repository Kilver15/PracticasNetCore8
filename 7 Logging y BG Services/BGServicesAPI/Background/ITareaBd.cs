
namespace BGServicesAPI.Background
{
    public class ITareaBd : IHostedLifecycleService
    {
        private readonly ILogger<ITareaBd> _logger;
        public ITareaBd(ILogger<ITareaBd> logger)
        {
            _logger = logger;
        }
        public Task StartingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Antes de iniciar.");
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando.");
            return Task.CompletedTask;
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tarea iniciada.");
            return Task.CompletedTask;
        }
        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Antes de detener.");
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deteniendo.");
            return Task.CompletedTask;
        }

        public Task StoppedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tarea detenida.");
            return Task.CompletedTask;
        }
    }
}
