using System.Net;
using Bookfy.Authors.Api.Boundaries;
using Bookfy.Authors.Api.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Authors.Api.Adapters
{
    public static class AuthorRoutesExtensions
    {

        public static RouteGroupBuilder MapAuthorRoutes(this RouteGroupBuilder app)
        {
            var authorsGroup = app.MapGroup("/authors");

            authorsGroup.MapPost("",
                async (
                    IAuthorUseCase authorUseCase,
                    [FromBody] CreateAuthor input,
                    CancellationToken ct) =>
                    {
                        var result = await authorUseCase.Create(input, ct);
                        return JsonFromResult(result);
                    });

            authorsGroup.MapPut("{id:guid}",
                async (
                    IAuthorUseCase authorUseCase,
                    CancellationToken ct,
                    [FromRoute] Guid id,
                    [FromBody] UpdateAuthor input) =>
                    {
                        input.Id = id;
                        var result = await authorUseCase.Update(input, ct);
                        return JsonFromResult(result);
                    }
            );

            authorsGroup.MapGet("{id:guid}",
                async (
                    IAuthorUseCase authorUseCase,
                    CancellationToken ct,
                    [AsParameters] GetAuthorById input) =>
                    {
                        var result = await authorUseCase.GetById(input, ct);
                        return JsonFromResult(result);
                    }
                );
            authorsGroup.MapGet("search", async (
                CancellationToken ct,
                IAuthorUseCase authorUseCase,
                [AsParameters] SearchAuthors input) => 
                {
                    var result = await authorUseCase.Get(input, ct);
                    return JsonFromResult(result);
                });

            authorsGroup.MapDelete("{id:guid}",
                async (
                    IAuthorUseCase authorUseCase,
                    CancellationToken ct,
                    [AsParameters] DeleteAuthor input) =>
                    {
                        var result = await authorUseCase.Delete(input, ct);
                        return JsonFromResult(result);
                    }
                );


            return app;
        }

        private static IResult JsonFromResult<T>(Result<T> result) =>
            result.Code == (int)HttpStatusCode.NoContent
                ? Results.NoContent()
                : Results.Json(
                    data: result,
                    statusCode: result.Code);
    }
}