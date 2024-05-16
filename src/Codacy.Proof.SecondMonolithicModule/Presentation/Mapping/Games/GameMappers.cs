using Codacy.Proof.SecondMonolithicModule.Contracts.Games;
using Codacy.Proof.SecondMonolithicModule.Domain.Games;

namespace Codacy.Proof.SecondMonolithicModule.Presentation.Mapping.Games;

internal static class GameMappers
{
    internal static GetGameIdResponse MapToGetGameIdResponse(this GameId gameId)
    {
        return new GetGameIdResponse(gameId.Value);
    }
}
