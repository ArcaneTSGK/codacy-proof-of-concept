using Codacy.Proof.FirstMonolithicModule.Domain.Players;

namespace Codacy.Proof.FirstMonolithicModule.Application.Interfaces;

internal interface IReadOnlyPlayerRepository
{
    Task<PlayerId> GetPlayerIdByLogin(Login login, CancellationToken cancellationToken = default);
}
