using Codacy.Proof.SecondMonolithicModule.Application.Interfaces;
using Codacy.Proof.SecondMonolithicModule.Domain.Games;
using ErrorOr;
using MediatR;

namespace Codacy.Proof.SecondMonolithicModule.Application.UseCases.GetGameId.Query;

internal class GetGameIdQueryHandler : IRequestHandler<GetGameIdQuery, ErrorOr<GameId>>
{
    private readonly IReadOnlyGameRepository _gameRepository;

    public GetGameIdQueryHandler(IReadOnlyGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<ErrorOr<GameId>> Handle(GetGameIdQuery request, CancellationToken cancellationToken)
    {
        // Simulate processing
        if (request.Error)
            return Error.NotFound("Game Not Found");

        return await _gameRepository.GetGameIdByGameName(new GameName(request.GameName), cancellationToken);
    }
}
