using Codacy.Proof.SecondMonolithicModule.Application.Interfaces;
using Codacy.Proof.SecondMonolithicModule.Domain.Games;

namespace Codacy.Proof.SecondMonolithicModule.Infrastructure;

internal class ReadOnlyGameRepository : IReadOnlyGameRepository
{
    public async Task<GameId> GetGameIdByGameName(GameName gameName, CancellationToken cancellationToken = default)
    {
        var name = gameName.Value;

        // Simulate processing
        if (name.Length != 0)
            await Task.Delay(100, cancellationToken);

        return new GameId(17);
    }
}
