using ChessComFramework.Core.Clients;
using ChessComFramework.Core.Config;
using FluentAssertions;
using ChessComFramework.Core.Helpers;
using ChessComFramework.Tests.Helpers;

namespace ChessComFramework.Tests;

[TestClass]
public class PlayerTests
{
    private ChessComClient? _client;

    [TestInitialize]
    public void Setup()
    {
        var config = TestConfiguration.Load();
        _client = new ChessComClient(config);
    }

    [TestMethod]
    public async Task GetPlayer_ValidUsername_ReturnsCorrectPlayer()
    {
        var player = await _client!.GetPlayerAsync("hikaru");

        Assert.IsNotNull(player);
        player.Username.Should().Be("hikaru");
        player.PlayerId.Should().BePositive();
        player.Followers.Should().BePositive();
    }

    [TestMethod]
    public async Task GetPlayer_InvalidUsername_ThrowsHttpException()
    {
        var act = async () => await _client!.GetPlayerAsync("invalidusername1234567890");

        await act.Should().ThrowAsync<HttpRequestException>();
    }

    [TestMethod]
    public async Task GetPlayerStats_AllPlayers_ReturnsValidStats()
    {
        var players = TestDataHelper.LoadPlayers();

        foreach (var playerData in players)
        {
            var stats = await _client!.GetPlayerStatsAsync(playerData.Username);

            Assert.IsNotNull(stats, $"Stats should not be null for {playerData.Username}");

            if (playerData.HasRapid)
            {
                PlayerStatsHelper.AssertValidGameMode(stats.Rapid, "Rapid");
            }
            else
            {
                Assert.IsNull(stats.Rapid, $"{playerData.Username} should not have rapid stats");
            }

            if (playerData.HasBlitz)
            {
                PlayerStatsHelper.AssertValidGameMode(stats.Blitz, "Blitz");

            }
            else
            {
                Assert.IsNull(stats.Blitz, $"{playerData.Username} should not have blitz stats");
            }

            if (playerData.HasBullet)
            {
                PlayerStatsHelper.AssertValidGameMode(stats.Bullet, "Bullet");

            }
            else
            {
                Assert.IsNull(stats.Bullet, $"{playerData.Username} should not have bullet stats");
            }
        }
    }

    [TestMethod]
    public async Task GetPlayerStats_MagnusVsHikaru()
    {
        var players = TestDataHelper.LoadPlayers();
        var topPlayers = players.Where(p => p.Tags.Contains("top_player")).ToList();

        var stats1 = await _client!.GetPlayerStatsAsync(topPlayers[0].Username);
        var stats2 = await _client!.GetPlayerStatsAsync(topPlayers[1].Username);

        PlayerStatsHelper.AssertValidGameMode(stats1!.Rapid, "Rapid");
        PlayerStatsHelper.AssertValidGameMode(stats2!.Rapid, "Rapid");

        stats1.Rapid!.Best!.Rating.Should().NotBe(stats2.Rapid!.Best!.Rating);
    }
}