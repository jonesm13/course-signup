namespace Process.Tests.Doubles
{
    using System;
    using Microsoft.Extensions.Logging;

    public class NopLogger : ILogger
    {
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new Disposer();
        }

        public class Disposer : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
