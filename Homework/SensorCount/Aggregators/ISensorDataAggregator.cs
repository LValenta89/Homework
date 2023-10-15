using Homework.Measurement.Data;
using Homework.SensorCount.Data;

namespace Homework.SensorCount.Aggregators;

/// <summary>
/// Counts number of provided measurements for each sensor/device as well as sum of measurements for all sensors
/// </summary>
public interface ISensorDataAggregator
{
    /// <summary>
    /// Counts newly added measurements to already existing statistic
    /// </summary>
    /// <param name="data">Newly added measurements</param>
    /// <remarks>Method cannot verify if newly added data were counted of not. It is a responsibility of called to
    /// ensure data consistency.</remarks>
    void CountSensorData(MeasurementData[] data);
    
    /// <summary>
    /// Current counts of measurements
    /// </summary>
    SensorDataCount SensorDataCount { get; }
}