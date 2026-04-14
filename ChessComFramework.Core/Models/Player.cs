using Newtonsoft.Json;

namespace ChessComFramework.Core.Models;

public class Player
{
    [JsonProperty("username")]
    public string? Username { get; set; }

    [JsonProperty("player_id")]
    public int PlayerId { get; set; }

    [JsonProperty("followers")]
    public int Followers { get; set; }

    [JsonProperty("country")]
    public string? Country { get; set; }

    [JsonProperty("last_online")]
    public long LastOnline { get; set; }

    [JsonProperty("joined")]
    public long Joined { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }
}