using Codacy.Proof.FirstMonolithicModule.Domain.Players;
using ErrorOr;
using MediatR;

namespace Codacy.Proof.FirstMonolithicModule.Application.UseCases.GetPlayerId.Query;

internal record GetPlayerIdQuery(string Login, bool Error) : IRequest<ErrorOr<PlayerId>>;
