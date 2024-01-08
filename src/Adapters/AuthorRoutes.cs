using Bookfy.Authors.Api.Boundaries;
using Bookfy.Authors.Api.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Authors.Api.Adapters
{
    public static class AuthorRoutesExtensions
    {

        public static RouteGroupBuilder MapAuthorRoutes(this RouteGroupBuilder app)
        {
            app.MapGroup("/authors")
                 .MapPost("authors", async (
                    IAuthorUseCase authorUseCase,
                    [FromBody] CreateAuthor input,
                    CancellationToken ct) =>
                    {
                        var result = await authorUseCase.Create(input, ct);
                        return Results.Json(
                            data: result.Success 
                                ? result.Data 
                                : result.Message,
                            statusCode: result.Code);
                    });

            return app;
        }
    }
}