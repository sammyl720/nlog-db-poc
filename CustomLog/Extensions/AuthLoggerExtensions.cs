using System.Runtime.CompilerServices;
using CustomLog.Models;
using Microsoft.Extensions.Logging;

namespace CustomLog.Extensions;

public static class AuthLoggerExtensions
{
    public static void LogAuthAttempt(
        this ILogger logger,
        string email,
        string? userId,
        string actionType,
        [CallerMemberName] string? memberName = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int lineNumber = 0)
    {
        AuthLogEntry logEntry = new AuthLogEntry()
        {
            Email = email,
            UserId = userId,
            ActionType = actionType,
            CallerMemberName = memberName,
            CallerFilePath = filePath,
            CallerLineNumber = lineNumber
        };

        var authLogger = LoggerMessage.Define<AuthLogEntry>(
            LogLevel.Information,
            new EventId(1001, "AuthAttempt"),
            "User authentication action: {@Entry}");
        authLogger(logger, logEntry, null);
        //
        // logger.Log(
        //     LogLevel.Information,
        //     new EventId(1001, "AuthAttempt"),
        //     logEntry,
        //     null,
        //     (state, ex) => $"User {state.Email} attempted {state.ActionType}");
    }
}