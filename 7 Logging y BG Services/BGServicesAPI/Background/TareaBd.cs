
namespace BGServicesAPI.Background
{
    public class TareaBd : BackgroundService
    {
        private readonly ILogger<TareaBd> _logger;
        public TareaBd(ILogger<TareaBd> logger)
        {
            _logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
                {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("TareaBd en ejecución.");
                    await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Detenida");
            }
        }
    }
}
