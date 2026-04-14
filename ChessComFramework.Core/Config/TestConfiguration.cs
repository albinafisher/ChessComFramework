namespace ChessComFramework.Core.Config;

public class TestConfiguration
{
    public string BaseUrl { get; init; } = "https://api.chess.com/pub";
    public int TimeoutSeconds { get; init; } = 30;

    public static TestConfiguration Load() => new();
}