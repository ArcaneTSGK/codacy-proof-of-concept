using Codacy.Proof.FirstMonolithicModule.Contracts.Players;
using Codacy.Proof.FirstMonolithicModule.Domain.Players;

namespace Codacy.Proof.FirstMonolithicModule.Presentation.Mapping.Players;

internal static class PlayerMappers
{
    internal static GetPlayerIdResponse MapToGetPlayerIdResponse(this PlayerId playerId)
    {
        return new GetPlayerIdResponse { PlayerId = playerId.Value };
    }
}
