using Homework.Measurement.Generators;
using Homework.Measurement.Services;
using Homework.SensorCount.Aggregators;
using Homework.SensorCount.Services;

namespace Homework.Extensions;

/// <summary>
/// Set of custom extensions methods for IServiceCollection so that Program.cs stays clean
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// DI registrations
    /// </summary>
    /// <param name="services">Service collection</param>
    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IMeasurementGenerator, MeasurementGenerator>();
        services.AddSingleton<ISensorDataAggregator, SensorDataAggregator>();
    }

    /// <summary>
    /// Hosted services registrations
    /// </summary>
    /// <param name="services">Service collection</param>
    public static void RegisterHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<GeneratorService>();
        services.AddHostedService<SensorCountService>();
    }
}