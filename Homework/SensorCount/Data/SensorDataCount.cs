namespace Homework.SensorCount.Data;

public class SensorDataCount
{
    /// <summary>
    /// Stores number of counts per sensor/device
    /// </summary>
    public Dictionary<string, int> MeasurementsBySensor { get; set; } = new();
    
    /// <summary>
    /// Stores total count of all measurements
    /// </summary>
    public int TotalMeasurements { get; set; }
}