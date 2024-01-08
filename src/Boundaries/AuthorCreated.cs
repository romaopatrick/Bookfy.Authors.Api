using Bookfy.Authors.Api.Domain;

namespace Bookfy.Authors.Api.Boundaries
{
    public class AuthorCreated(Author author)
    {
        public Guid Id { get; init; } = author.Id;
        public string FullName { get; init; } = author.FullName;
        public string? Nickname { get; init; } = author.Nickname;
        public DateTime CreatedAt { get; init; } = author.CreatedAt;
        public DateTime UpdatedAt { get; init; } = author.UpdatedAt;
    }
}