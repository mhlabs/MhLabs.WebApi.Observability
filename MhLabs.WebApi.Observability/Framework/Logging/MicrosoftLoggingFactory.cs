using Microsoft.Extensions.Logging;
using Serilog;

namespace MhLabs.WebApi.Observability.Framework.Logging
{
    public static class MicrosoftLoggingFactory
    {
        public static readonly ILoggerFactory LoggerFactory;

        static MicrosoftLoggingFactory()
        {
            if (LoggerFactory == null)
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddSerilog(SerilogFactory.Instance);
                LoggerFactory = loggerFactory;
            }
        }

        public static ILogger<T> Create<T>()
        {
            return LoggerFactory.CreateLogger<T>();
        }
    }
}