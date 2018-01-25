using System;
using Core.Logging;

namespace Logging {
    public class ConsoleLogger : ILog {
        public ConsoleLogger() { }

        public void LogInfo(string message) {
            Console.WriteLine(message);
        }
    }
}