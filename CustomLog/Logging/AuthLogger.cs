using CustomLog.Models;
using Microsoft.Extensions.Logging;

namespace CustomLog.Logging;

public class AuthLogger(string categoryName, IServiceProvider serviceProvider) : ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        // Now, check if this is an auth log with expected state
        if (state is AuthLogEntry entry)
        {
            // Here, you would inject your DbContext and save to the DB
            Console.WriteLine($"[CustomAuthLogger] {entry.Timestamp:o} - {entry.ActionType} - {entry.Email} ({entry.UserId}) at {entry.CallerMemberName} in {entry.CallerFilePath}:{entry.CallerLineNumber}");
        }
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) => default!;
}