using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Bookfy.Authors.Api.Boundaries;
using Bookfy.Authors.Api.Domain;

namespace Bookfy.Authors.Api.Ports
{
    public interface IAuthorRepository
    {
        Task<Author> First(Expression<Func<Author, bool>> predicate, CancellationToken ct);
        Task<Paginated<Author>> Get(Expression<Func<Author, bool>> filter, long skip, long take, CancellationToken ct);
        Task<Author> Create(Author author, CancellationToken ct);
        Task<Author> Update(Author author, CancellationToken ct);
        Task Delete(Guid id, CancellationToken ct);
    }
}