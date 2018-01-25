using Microsoft.Extensions.Logging;
using Core.Logging;

namespace Logging
{
    public class Logger : ILog
    {
        private ILogger log;

        public Logger(ILoggerFactory logFactory)
        {
            this.log = logFactory.CreateLogger("TemplateApplication");
        }

        public void LogInfo(string message)
        {
            this.log.LogInformation(message);
        }
    }
}