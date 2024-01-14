using Bookfy.Authors.Api.Boundaries;
using Bookfy.Authors.Api.Domain;

namespace Bookfy.Authors.Api.Ports
{
public interface IAuthorUseCase
    {
        Task<Result<AuthorCreated>> Create(CreateAuthor input, CancellationToken ct);
        Task<Result<AuthorUpdated>> Update(UpdateAuthor input, CancellationToken ct);
        Task<Result<Author>> GetById(GetAuthorById input, CancellationToken ct);
        Task<Result<Paginated<Author>>> Get(SearchAuthors input, CancellationToken ct);
        Task<Result<AuthorDeleted>> Delete(DeleteAuthor input, CancellationToken ct);
    }
}