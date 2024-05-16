using System.Text.Json.Serialization;

namespace Codacy.Proof.FirstMonolithicModule.Contracts.Players;

public record GetPlayerIdResponse
{
    [JsonPropertyName("playerId")]
    public string PlayerId { get; init; } = string.Empty;
};
