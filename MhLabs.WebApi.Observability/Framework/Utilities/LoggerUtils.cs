using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace MhLabs.WebApi.Observability.Framework.Utilities
{
    public static class LoggerUtils
    {
        /// <summary>
        /// Creates a new instance of <pre>ILogger<T></pre>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Mock<ILogger<T>> LoggerMock<T>() where T : class
        {
            return new Mock<ILogger<T>>();
        }

        /// <summary>
        /// Verify that <see cref="string"/> has been logged one time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="loggerMock"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message)
        {
            loggerMock.VerifyLog(level, message, Times.Once());
        }

        /// <summary>
        /// Verify that <see cref="string"/> has been logged x number of <see cref="Times"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="loggerMock"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="times"></param>
        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message, Times times)
        {
            loggerMock.Verify(
                l => l.Log(
                    level, 
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                times);
        }
    }
}
