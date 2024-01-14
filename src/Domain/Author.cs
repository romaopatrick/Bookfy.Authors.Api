namespace Bookfy.Authors.Api.Domain
{
    public class Author
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string FullName { get; set; }
        public string? Nickname { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}