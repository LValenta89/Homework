using Homework.Measurement.Data;

namespace Homework.Measurement.Generators;

/// <summary>
/// Generates random measurement values for sensors/devices
/// </summary>
public interface IMeasurementGenerator
{
    /// <summary>
    /// Retrieves generated measurements
    /// </summary>
    /// <param name="since">Point in time from which the data will be retrieved</param>
    /// <returns>Callection of all measurement data for all sensors/devices</returns>
    MeasurementData[] GetSensorData(DateTime since);
    
    /// <summary>
    /// Starts background generation of mock data
    /// </summary>
    void StartGenerating();
    
    /// <summary>
    /// Stops background generation of mock data
    /// </summary>
    void StopGenerating();
}