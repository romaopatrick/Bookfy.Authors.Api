using System.Text.Json.Serialization;
using Bookfy.Authors.Api.Domain;

namespace Bookfy.Authors.Api.Boundaries
{
    public class UpdateAuthor
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? FullName { get; init; }
        public string? Nickname { get; init; }
    }
}