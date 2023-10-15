using Homework.Measurement.Data;
using Homework.Measurement.Generators;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

/// <summary>
/// Generates mock data 
/// </summary>
[ApiController]
[Route("[controller]")]
public class MockController : ControllerBase
{
    private readonly IMeasurementGenerator measurementGenerator;

    public MockController(IMeasurementGenerator measurementGenerator)
    {
        this.measurementGenerator = measurementGenerator;
    }

    /// <summary>
    /// Generates mock sensor/device measurements
    /// </summary>
    /// <param name="since">Controls from which point in time is should return the data</param>
    /// <returns>Mock measurement data</returns>
    [HttpGet("SensorData")]
    public IEnumerable<MeasurementData> GetSensorData(DateTime since)
    {
        return measurementGenerator.GetSensorData(since);
    }
}