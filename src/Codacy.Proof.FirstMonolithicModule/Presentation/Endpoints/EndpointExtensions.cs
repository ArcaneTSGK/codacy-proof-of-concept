using Codacy.Proof.FirstMonolithicModule.Presentation.Endpoints.Players.GetPlayerId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Codacy.Proof.FirstMonolithicModule.Presentation.Endpoints;

internal static class EndpointExtensions
{
    internal static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var players = app.NewVersionedApi("Players");

        var playerId = players
            .MapGroup("")
            .HasApiVersion(1);

        playerId.MapGetPlayerId();

        return app;
    }
}
