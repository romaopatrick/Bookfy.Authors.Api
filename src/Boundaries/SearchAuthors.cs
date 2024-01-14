using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Authors.Api.Boundaries;

public class SearchAuthors
{
    [FromQuery(Name = "searchTerm")]
    public string? SearchTerm { get; init; } = "";
    [FromQuery(Name = "skip")]
    public long? Skip { get; init; }
    [FromQuery(Name = "take")]
    public long? Take { get; init; }
}