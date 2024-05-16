using Codacy.Proof.SecondMonolithicModule.Domain.Games;

namespace Codacy.Proof.SecondMonolithicModule.Application.Interfaces;

internal interface IReadOnlyGameRepository
{
    Task<GameId> GetGameIdByGameName(GameName gameName, CancellationToken cancellationToken = default);
}
