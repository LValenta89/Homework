using Homework.Measurement.Data;
using Homework.SensorCount.Data;

namespace Homework.SensorCount.Aggregators;

/// <inheritdoc/>
public class SensorDataAggregator : ISensorDataAggregator
{
    public SensorDataCount SensorDataCount { get; } = new();
    
    public void CountSensorData(MeasurementData[] data)
    {
        var newSensorCounts = data
            .GroupBy(e => e.SensorName)
            .Select(e => new {e.Key, Count = e.Count()});

        foreach (var sensorCount in newSensorCounts)
        {
            SensorDataCount.MeasurementsBySensor.TryGetValue(sensorCount.Key, out int currentCount);
            SensorDataCount.MeasurementsBySensor[sensorCount.Key] = currentCount + sensorCount.Count;
        }

        SensorDataCount.TotalMeasurements += newSensorCounts.Sum(e => e.Count);
    }
}