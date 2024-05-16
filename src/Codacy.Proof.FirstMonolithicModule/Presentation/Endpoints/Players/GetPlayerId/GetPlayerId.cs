using Codacy.Proof.FirstMonolithicModule.Application.UseCases.GetPlayerId.Query;
using Codacy.Proof.FirstMonolithicModule.Contracts.Players;
using Codacy.Proof.FirstMonolithicModule.Presentation.Mapping.Players;
using Codacy.Proof.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Codacy.Proof.FirstMonolithicModule.Presentation.Endpoints.Players.GetPlayerId;

internal static class GetPlayerId
{
    const string Name = nameof(GetPlayerId);

    internal static IEndpointRouteBuilder MapGetPlayerId(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/players/me/id", async (
            ISender mediator, CancellationToken cancellationToken) =>
        {
            const bool throwError = false;

            var result = await mediator.Send(new GetPlayerIdQuery("removeMe", throwError),
                cancellationToken);

            return result.Match(playerId => TypedResults.Ok(playerId.MapToGetPlayerIdResponse()),
                ProblemExtensions.Problem);
        })
        .WithName(Name)
        .MapToApiVersion(1)
        .Produces<GetPlayerIdResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        return app;
    }
}
