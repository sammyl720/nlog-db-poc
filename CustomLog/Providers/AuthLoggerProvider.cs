using CustomLog.Logging;
using Microsoft.Extensions.Logging;

namespace CustomLog.Providers;

public class AuthLoggerProvider(IServiceProvider serviceProvider) : ILoggerProvider
{
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new AuthLogger(categoryName, serviceProvider);
    }
}