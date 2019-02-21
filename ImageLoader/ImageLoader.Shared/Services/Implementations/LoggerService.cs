using NLog;
using System;

namespace ImageLoader.Shared.Services.Implementations
{
    internal class LoggerService : ILoggerService
    {
        private static Lazy<Logger> _lazyLogger = new Lazy<Logger>(CreateLogger);

        private const string LoggerName = "sharedLogger";

        private readonly Logger _logger;

        public LoggerService()
        {
            _logger = _lazyLogger.Value;
        }

        private static Logger CreateLogger()
        {
            return LogManager.GetLogger(LoggerName);
        }

        public void LogError(Exception ex, string message)
        {
            if (_logger.IsErrorEnabled)
            {
                _logger.Error(ex, message);
            }
        }
    }
}
