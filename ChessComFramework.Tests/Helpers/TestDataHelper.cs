using Newtonsoft.Json;

namespace ChessComFramework.Core.Helpers;

public class PlayerTestData
{
    public string Username { get; set; } = string.Empty;
    public bool HasBlitz { get; set; }
    public bool HasBullet { get; set; }
    public bool HasRapid { get; set; }

    public List<string> Tags { get; set; } = new();
}

public class PlayersTestData
{
    public List<PlayerTestData> Players { get; set; } = new();
}

public static class TestDataHelper
{
    public static List<PlayerTestData> LoadPlayers()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var path = Path.Combine(basePath, "testdata", "players.json");
        var json = File.ReadAllText(path);
        var data = JsonConvert.DeserializeObject<PlayersTestData>(json);
        return data?.Players ?? new List<PlayerTestData>();
    }
}