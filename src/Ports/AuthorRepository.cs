using Bookfy.Authors.Api.Domain;

namespace Bookfy.Authors.Api.Ports
{
    public interface IAuthorRepository
    {
        Task<Author?> FirstByFullName(string fullname, CancellationToken ct); 
        Task<Author> Create(Author author, CancellationToken ct);
    }
}