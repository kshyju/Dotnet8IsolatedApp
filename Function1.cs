using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp6
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger started: {DateTime.Now}");
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogWarning($"A Custom Warning log entry at: {DateTime.Now}");
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
                _logger.LogInformation($"Custom Info log entry at: {DateTime.Now}");                
            }
        }
    }
}
