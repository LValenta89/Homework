namespace Homework.Measurement.Data;

/// <summary>
/// Represents measurement data from a sensor/device
/// </summary>
/// <param name="Created">UTC timestamp representing when the measurement was created</param>
/// <param name="SensorName">Name of the sensor/device which produced the measurement</param>
/// <param name="Value">Measured value</param>
public record MeasurementData(DateTime Created, string SensorName, int Value);