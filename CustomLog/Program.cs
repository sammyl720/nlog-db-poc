using Microsoft.Data.Sqlite;
using NLog;

EnsureDatabase();

NLog.Common.InternalLogger.LogToConsole = true;
NLog.Common.InternalLogger.LogLevel = LogLevel.Debug;

LogManager.LoadConfiguration("nlog.config");

var logger = LogManager.GetCurrentClassLogger();

LogAuthAttempt(logger, "user1@example.com", "user-123", "Login");
LogAuthAttempt(logger, "user2@example.com", null, "Register");
LogAuthAttempt(logger, "user3@example.com", "user-789", "PasswordChange");

Console.WriteLine("Logging complete. Check authlogs.db for results.");

static void EnsureDatabase()
{
    string dbFile = "auth-logs.db";
    bool createTable = !File.Exists(dbFile);

    using (var connection = new SqliteConnection($"Data Source={dbFile}"))
    {
        connection.Open();
        if (createTable)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS AuthLogs (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT,
    UserId TEXT,
    ActionType TEXT,
    CallerMemberName TEXT,
    CallerFilePath TEXT,
    CallerLineNumber INTEGER,
    Timestamp TEXT
);";
            cmd.ExecuteNonQuery();
        }
    }
}

// Helper: Logs with properties and caller info
static void LogAuthAttempt(
    Logger logger,
    string email,
    string userId,
    string actionType,
    [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
    [System.Runtime.CompilerServices.CallerFilePath] string filePath = "",
    [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
{
    logger.WithProperty("Email", email)
        .WithProperty("UserId", userId)
        .WithProperty("ActionType", actionType)
        .WithProperty("CallerMemberName", memberName)
        .WithProperty("CallerFilePath", filePath)
        .WithProperty("CallerLineNumber", lineNumber)
        .Info("User authentication event");
}

