using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppDemo;

public class AzureTimerFunction
{
    private readonly ILogger _logger;

    public AzureTimerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<AzureTimerFunction>();
    }

    [Function("AzureTimerFunction")]
    public void Run([TimerTrigger("%TimerSchedule%")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}