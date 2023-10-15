using System;
using FluentAssertions;
using Homework.Measurement.Data;
using Homework.SensorCount.Aggregators;
using Homework.Tests.Contexts;
using TechTalk.SpecFlow;

namespace Homework.Tests;

[Binding]
public class SensorDataAggregatorStepDefinitions
{
    private readonly SensorDataAggregatorContext context;

    public SensorDataAggregatorStepDefinitions(SensorDataAggregatorContext context)
    {
        this.context = context;
    }

    [Given(@"that data sensor aggregator \(DSA\) is initialized")]
    public void GivenThatDataSensorAggregatorDsaIsInitialized()
    {
        // Let's insitantiate the implementation that we really want to test
        // Given gives us opportunity to setup mocks etc.
        context.SensorDataAggregator = new SensorDataAggregator();
    }

    [When(@"DSA receives (.*) message\(s\) for (.*)")]
    public void WhenDsaReceivesMessageForSensor(int messagesCount, string sensorName)
    {
        MeasurementData[] data = new MeasurementData[messagesCount];
        for (int i = 0; i < messagesCount; i++)
        {
            data[i] = new MeasurementData(DateTime.UtcNow, sensorName, 42);
        }
        
        context.SensorDataAggregator.CountSensorData(data);
    }

    [Then(@"the result should be (.*) for (.*)")]
    public void ThenTheResultShouldBeForSensor(int messagesCount, string sensorName)
    {
        context.SensorDataAggregator.SensorDataCount.MeasurementsBySensor.ContainsKey(sensorName).Should()
            .BeTrue($"Aggregator was called with sensor name {sensorName}");
        context.SensorDataAggregator.SensorDataCount.MeasurementsBySensor[sensorName].Should()
            .Be(messagesCount, $"Total messages received for {sensorName} was {messagesCount}");
    }

    [Then(@"total messages received should be (.*)")]
    public void ThenTotalMessagesReceivedShouldBe(int totalMessagesCount)
    {
        context.SensorDataAggregator.SensorDataCount.TotalMeasurements.Should()
            .Be(totalMessagesCount, $"Total messages received was {totalMessagesCount}");
    }
}