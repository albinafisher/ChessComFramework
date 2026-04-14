using ChessComFramework.Core.Config;
using ChessComFramework.Core.Models;
using Newtonsoft.Json;

namespace ChessComFramework.Core.Clients;

public class ChessComClient
{
    private readonly HttpClient _httpClient;
    private readonly TestConfiguration _config;

    public ChessComClient(TestConfiguration config)
    {
        _config = config;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_config.BaseUrl),
            Timeout = TimeSpan.FromSeconds(_config.TimeoutSeconds)
        };
        _httpClient.DefaultRequestHeaders.Add(
            "User-Agent", "ChessComFramework/1.0"
        );
    }

    public async Task<Player?> GetPlayerAsync(string username)
    {
        var response = await _httpClient.GetAsync($"/pub/player/{username}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Player>(content);
    }

    public async Task<PlayerStats?> GetPlayerStatsAsync(string username)
    {
        var response = await _httpClient.GetAsync($"/pub/player/{username}/stats");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PlayerStats>(content);
    }
}

