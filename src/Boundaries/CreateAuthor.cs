namespace Bookfy.Authors.Api.Boundaries
{
    public class CreateAuthor
    {
        public required string FullName { get; init; }
        public string? Nickname { get; init; }
    }
}