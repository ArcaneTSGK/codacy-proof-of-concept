using Codacy.Proof.FirstMonolithicModule.Application.Interfaces;
using Codacy.Proof.FirstMonolithicModule.Application.UseCases.GetPlayerId.Query;
using Codacy.Proof.FirstMonolithicModule.Domain.Players;
using FluentAssertions;
using NSubstitute;

namespace Codacy.Proof.FirstMonolithicModule.Tests.Unit.Application.UseCases.GetPlayerId;

public class GetPlayerIdTests
{
    private readonly IReadOnlyPlayerRepository _playerRepository;
    private readonly GetPlayerIdQueryHandler _sut;

    public GetPlayerIdTests()
    {
        _playerRepository = Substitute.For<IReadOnlyPlayerRepository>();
        _sut = new GetPlayerIdQueryHandler(_playerRepository);
    }

    [Fact]
    public async Task Handle_WithPlayerId_ReturnsPlayerId()
    {
        // Arrange
        var playerId = new PlayerId("12345678");
        var request = new GetPlayerIdQuery("login", false);

        _playerRepository.GetPlayerIdByLogin(Arg.Any<Login>(), Arg.Any<CancellationToken>())
            .Returns(playerId);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Value.Should().Be(playerId);
    }

    [Fact]
    public async Task Handle_WithThrowError_ReturnsError()
    {
        // Arrange
        var request = new GetPlayerIdQuery("login", true);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.FirstError.Code.Should().Be("Player Not Found");
    }
}
