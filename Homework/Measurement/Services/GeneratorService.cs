using Homework.Measurement.Generators;

namespace Homework.Measurement.Services;

/// <summary>
/// Manages background generation of mock data
/// </summary>
public class GeneratorService : IHostedService
{
    private readonly ILogger<GeneratorService> logger;
    private readonly IMeasurementGenerator generator;

    public GeneratorService(ILogger<GeneratorService> logger, IMeasurementGenerator generator)
    {
        this.logger = logger;
        this.generator = generator;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Generator Service stared");
        generator.StartGenerating();
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Generator service ending");
        generator.StopGenerating();
        logger.LogInformation("Generator service ended");

        return Task.CompletedTask;
    }
}