using Codacy.Proof.FirstMonolithicModule.Contracts.Players;
using Codacy.Proof.FirstMonolithicModule.Domain.Players;
using Codacy.Proof.FirstMonolithicModule.Presentation.Mapping.Players;
using FluentAssertions;

namespace Codacy.Proof.FirstMonolithicModule.Tests.Unit.Presentation.Mapping;

public class PlayerMappersTests
{
    [Fact]
    public void MapToGetPlayerIdResponse_WithNullPlayerId_ThrowsArgumentNullException()
    {
        // Arrange
        PlayerId playerId = null!;

        // Act
        Action act = () => playerId.MapToGetPlayerIdResponse();

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void MapToGetPlayerIdResponse_WithPlayerId_ReturnsGetPlayerIdResponse()
    {
        // Arrange
        var playerId = new PlayerId("playerId");

        // Act
        var result = playerId.MapToGetPlayerIdResponse();

        // Assert
        result.Should().BeOfType<GetPlayerIdResponse>();
        result.PlayerId.Should().Be(playerId.Value);
    }
}
