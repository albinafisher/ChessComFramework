using Newtonsoft.Json;

namespace ChessComFramework.Core.Models;

public class PlayerStats
{
    [JsonProperty("chess_rapid")]
    public GameModeStats? Rapid { get; set; }

    [JsonProperty("chess_bullet")]
    public GameModeStats? Bullet { get; set; }

    [JsonProperty("chess_blitz")]
    public GameModeStats? Blitz { get; set; }
}

public class GameModeStats
{
    [JsonProperty("last")]
    public RatingInfo? Last { get; set; }

    [JsonProperty("best")]
    public RatingInfo? Best { get; set; }
}

public class RatingInfo
{
    [JsonProperty("rating")]
    public int Rating { get; set; }

    [JsonProperty("date")]
    public long Date { get; set; }
}