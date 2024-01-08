using Bookfy.Authors.Api.Boundaries;

namespace Bookfy.Authors.Api.Ports
{
public interface IAuthorUseCase
    {
        Task<Result<AuthorCreated>> Create(CreateAuthor input, CancellationToken ct);
    }
}