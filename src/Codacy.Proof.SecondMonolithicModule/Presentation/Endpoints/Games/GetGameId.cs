using Codacy.Proof.SecondMonolithicModule.Application.UseCases.GetGameId.Query;
using Codacy.Proof.SecondMonolithicModule.Contracts.Games;
using Codacy.Proof.SecondMonolithicModule.Presentation.Mapping.Games;
using Codacy.Proof.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Codacy.Proof.SecondMonolithicModule.Presentation.Endpoints.Games;

internal static class GetGameId
{
    const string Name = nameof(GetGameId);

    internal static IEndpointRouteBuilder MapGetGameId(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/games/me/id", async (
            ISender mediator, CancellationToken cancellationToken) =>
        {
            const bool throwError = false;

            var result = await mediator.Send(new GetGameIdQuery("removeMe", throwError),
                               cancellationToken);

            return result.Match(gameId => TypedResults.Ok(gameId.MapToGetGameIdResponse()),
                               ProblemExtensions.Problem);
        })
        .WithName(Name)
        .MapToApiVersion(1)
        .Produces<GetGameIdResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        return app;
    }
}
