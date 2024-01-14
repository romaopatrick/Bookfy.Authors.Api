using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Authors.Api.Boundaries;

public class DeleteAuthor
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}