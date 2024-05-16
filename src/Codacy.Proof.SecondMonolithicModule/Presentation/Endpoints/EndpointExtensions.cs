using Codacy.Proof.SecondMonolithicModule.Presentation.Endpoints.Games;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Codacy.Proof.SecondMonolithicModule.Presentation.Endpoints;

internal static class EndpointExtensions
{
    internal static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var games = app.NewVersionedApi("Games");

        var gameId = games
            .MapGroup("")
            .HasApiVersion(1);

        gameId.MapGetGameId();

        return app;
    }
}
