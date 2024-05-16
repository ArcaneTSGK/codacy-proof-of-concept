using Codacy.Proof.FirstMonolithicModule.Application.Interfaces;
using Codacy.Proof.FirstMonolithicModule.Domain.Players;
using ErrorOr;
using MediatR;

namespace Codacy.Proof.FirstMonolithicModule.Application.UseCases.GetPlayerId.Query;

internal class GetPlayerIdQueryHandler : IRequestHandler<GetPlayerIdQuery, ErrorOr<PlayerId>>
{
    private readonly IReadOnlyPlayerRepository _playerRepository;

    public GetPlayerIdQueryHandler(IReadOnlyPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<ErrorOr<PlayerId>> Handle(GetPlayerIdQuery request, CancellationToken cancellationToken)
    {

        // Simulate processing
        if (request.Error)
            return Error.NotFound("Player Not Found");

        return await _playerRepository.GetPlayerIdByLogin(new Login(request.Login), cancellationToken);
    }
}
