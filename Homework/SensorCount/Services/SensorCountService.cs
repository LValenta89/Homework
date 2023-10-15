using Homework.Measurement.Data;
using Homework.Measurement.Generators;
using Homework.SensorCount.Aggregators;

namespace Homework.SensorCount.Services;

/// <summary>
/// Manages background retrieval of measurement data in periodic intervals
/// </summary>
public class SensorCountService : IHostedService, IDisposable
{
    private readonly ILogger<SensorCountService> logger;
    private readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7152"),
    };

    private readonly ISensorDataAggregator sensorDataAggregator;
    private Timer? timer;
    private DateTime lastSeenData;

    public SensorCountService(ILogger<SensorCountService> logger, ISensorDataAggregator sensorDataAggregator)
    {
        this.logger = logger;
        this.sensorDataAggregator = sensorDataAggregator;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Sensor Count service started");

        timer = new Timer(RequestSensorData, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(1));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Generator service ending");
        timer?.Change(Timeout.Infinite, 0);
        logger.LogInformation("Generator service ended");

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
    
    /// <summary>
    /// Tries to retrieve new measurement data. The first run obtains all results and subsequent calls only request
    /// data that were added after the previous run.
    /// </summary>
    /// <param name="state"></param>
    private void RequestSensorData(object? state)
    {
        try
        {
            MeasurementData[] measurements = httpClient.GetFromJsonAsync<MeasurementData[]>(
                                                 $"Mock/SensorData?since={lastSeenData.ToString("s") + "Z"}").GetAwaiter().GetResult() 
                                             ?? Array.Empty<MeasurementData>();

            lastSeenData = measurements.Max(e => e.Created);
            sensorDataAggregator.CountSensorData(measurements);
        }
        catch (Exception e)
        {
            // Should be LogError I just didn't want to show exceptions when the server is booting up, because the 
            // root cause of exception is just that. I'm requesting the data from the server while it is not up yet.
            logger.LogDebug(e, "Could not get sensor data");
        }
    }
}