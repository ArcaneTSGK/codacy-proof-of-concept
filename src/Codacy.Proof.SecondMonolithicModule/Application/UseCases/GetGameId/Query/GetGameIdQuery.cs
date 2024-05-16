using Codacy.Proof.SecondMonolithicModule.Domain.Games;
using ErrorOr;
using MediatR;

namespace Codacy.Proof.SecondMonolithicModule.Application.UseCases.GetGameId.Query;

internal record GetGameIdQuery(string GameName, bool Error) : IRequest<ErrorOr<GameId>>;
