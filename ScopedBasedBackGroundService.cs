using Governement_Public_Health_Care.DB_Context;
using Governement_Public_Health_Care;

public class ScopedBasedBackGroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly SharedDataService _sharedDataService;

    public ScopedBasedBackGroundService(IServiceProvider serviceProvider, SharedDataService sharedDataService)
    {
        _serviceProvider = serviceProvider;
        _sharedDataService = sharedDataService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_sharedDataService.ApiCallReceived)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<HealthCareContext>();
                    Console.WriteLine("New DB context instance being created due to API call.");
                }
                _sharedDataService.ApiCallReceived = false; // Reset the flag
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        // Custom start-up logic here
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        // Custom shut-down logic here
        await base.StopAsync(cancellationToken);
    }

}
