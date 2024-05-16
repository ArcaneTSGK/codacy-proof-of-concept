using System.Text.Json;
using Codacy.Proof.FirstMonolithicModule.Contracts.Players;
using Flurl;

namespace Codacy.Proof.FirstMonolithicModule.Tests.Integration.Endpoints.Players;

[Collection("Players")]
public class GetPlayerIdTests
{
    private readonly HttpClient _client;

    public GetPlayerIdTests(PlayersApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task GetPlayerId_ReturnsPlayerId()
    {
        const string GetPlayerIdEndpoint = "/api/players/me/id";

        // Arrange
        var uri = _client.BaseAddress
            .AppendPathSegment(GetPlayerIdEndpoint)
            .ToUri();

        var response = await _client.GetAsync(uri);

        // Act
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var playerIdResponse = JsonSerializer.Deserialize<GetPlayerIdResponse>(content);

        // Assert
        Assert.NotNull(playerIdResponse);
        Assert.Equal("10001010", playerIdResponse.PlayerId);
    }
}
