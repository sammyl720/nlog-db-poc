namespace CustomLog.Models;

public class AuthLogEntry
{
    public string? UserId { get; set; }
    public string Email { get; set; }
    public string ActionType { get; set; }
    public string? CallerMemberName { get; set; }
    public string? CallerFilePath { get; set; }
    public int? CallerLineNumber { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}