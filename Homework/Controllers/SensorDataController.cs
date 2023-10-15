using Homework.SensorCount.Aggregators;
using Homework.SensorCount.Data;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

/// <summary>
/// Controller for sensor/device measurements
/// </summary>
[ApiController]
[Route("[controller]")]
public class SensorDataController : ControllerBase
{
    private readonly ISensorDataAggregator aggregator;

    public SensorDataController(ISensorDataAggregator aggregator)
    {
        this.aggregator = aggregator;
    }

    /// <summary>
    /// Returns total number of device measurements as well as per sensor/device measurement count
    /// </summary>
    /// <returns>Measurement counts</returns>
    [HttpGet("Count")]
    public SensorDataCount GetDataCount()
    {
        return aggregator.SensorDataCount;
    }
}