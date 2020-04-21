using MhLabs.WebApi.Observability.Framework.Utilities;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MhLabs.WebApi.Observability.Tests
{
    public class LoggerUtilsTests
    {
        [Fact]
        public void TestLogInformation()
        {
            // arrange
            var loggerMock = LoggerUtils.LoggerMock<MyHandler>();
            var handler = new MyHandler(loggerMock.Object);
            var expected = "Hello information";

            // act
            handler.LogInformation(expected);

            // assert
            loggerMock.VerifyLog(LogLevel.Information, expected);
        }

        [Fact]
        public void TestLogWarning()
        {
            // arrange
            var loggerMock = LoggerUtils.LoggerMock<MyHandler>();
            var handler = new MyHandler(loggerMock.Object);
            var expected = "Hello warning";

            // act
            handler.LogWarning(expected);

            // assert
            loggerMock.VerifyLog(LogLevel.Warning, expected);
        }

        [Fact]
        public void TestLogError()
        {
            // arrange
            var loggerMock = LoggerUtils.LoggerMock<MyHandler>();
            var handler = new MyHandler(loggerMock.Object);
            var expected = "Hello error";

            // act
            handler.LogError(expected);

            // assert
            loggerMock.VerifyLog(LogLevel.Error, expected);
        }
    }

    public class MyHandler
    {
        private ILogger<MyHandler> _logger { get; }
        public MyHandler(ILogger<MyHandler> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string textToLog)
        {
            _logger.LogInformation(textToLog);
        }

        public void LogWarning(string textToLog)
        {
            _logger.LogWarning(textToLog);
        }

        public void LogError(string textToLog)
        {
            _logger.LogError(textToLog);
        }

    }
}
