using System.ComponentModel.DataAnnotations;

namespace Bookfy.Authors.Api.Boundaries
{
    public class CreateAuthor
    {
        [Required]
        public required string FullName { get; init; }
        public string? Nickname { get; init; }
    }
}