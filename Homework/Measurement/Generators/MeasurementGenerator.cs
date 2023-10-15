using System.Collections.Concurrent;
using Homework.Measurement.Data;

namespace Homework.Measurement.Generators;

/// <inheritdoc/>
public class MeasurementGenerator : IMeasurementGenerator
{
    private readonly ConcurrentDictionary<DateTime, MeasurementData> data = new();
    private readonly CancellationTokenSource source = new();
    private readonly CancellationToken token;
    private readonly List<Task> tasks = new();
    private readonly ConcurrentStack<int> sensors = new(Enumerable.Range(1, 10));

    public MeasurementGenerator()
    {
        token = source.Token;
    }

    public void StartGenerating()
    {
        TaskFactory factory = new TaskFactory(token);
        for (int taskCount = 1; taskCount <= sensors.Count; taskCount++)
        {
            tasks.Add(factory.StartNew(() =>
            {
#pragma warning disable CS4014
                GenerateAsync(token);
#pragma warning restore CS4014
            }, token));
        }
    }

    public void StopGenerating()
    {
        source.Cancel();
    }

    public MeasurementData[] GetSensorData(DateTime since)
    {
        return data.Where(e => e.Key > since)
            .Select(e => e.Value)
            .OrderBy(e => e.Created)
            .ToArray();
    }

    private async Task GenerateAsync(CancellationToken cancellationToken)
    {
        sensors.TryPop(out int sensorIndex);
        string sensorName = $"Sensor{sensorIndex}";
        TimeSpan delaySpan = TimeSpan.FromMilliseconds(sensorIndex * 2500);
        
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(delaySpan, cancellationToken);
            DateTime now = DateTime.UtcNow;
            data[now] = new MeasurementData(now, sensorName, Random.Shared.Next(0, 100));
        }
    }
}