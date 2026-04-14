using FluentAssertions;
using FluentAssertions.Execution;
using ChessComFramework.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessComFramework.Tests.Helpers;

public static class PlayerStatsHelper
{
    private const int MinRating = 100;
    private const int MaxRating = 4000;

    public static void AssertValidGameMode(GameModeStats? stats, string modeName)
    {
        using (new AssertionScope())
        {
            Assert.IsNotNull(stats, $"{modeName} stats should exist");
            Assert.IsNotNull(stats!.Best, $"{modeName} best rating should exist");
            stats.Best!.Rating.Should().BeInRange(MinRating, MaxRating,
                because: $"{modeName} rating should be realistic");
        }
    }
}