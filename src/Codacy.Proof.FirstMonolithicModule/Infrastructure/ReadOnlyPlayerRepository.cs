using Codacy.Proof.FirstMonolithicModule.Application.Interfaces;
using Codacy.Proof.FirstMonolithicModule.Domain.Players;

namespace Codacy.Proof.FirstMonolithicModule.Infrastructure;

internal class ReadOnlyPlayerRepository : IReadOnlyPlayerRepository
{
    public async Task<PlayerId> GetPlayerIdByLogin(Login login, CancellationToken cancellationToken = default)
    {
        var username = login.Value;

        // Simulate processing
        if (username.Length != 0)
            await Task.Delay(100, cancellationToken);

        return new PlayerId("10001010");
    }
}
