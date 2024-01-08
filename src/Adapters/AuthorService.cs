using Bookfy.Authors.Api.Boundaries;
using Bookfy.Authors.Api.Domain;
using Bookfy.Authors.Api.Ports;

namespace Bookfy.Authors.Api.Adapters
{
    public class AuthorService(IAuthorRepository repository) : IAuthorUseCase
    {
        private readonly IAuthorRepository _repository = repository;

        public async Task<Result<AuthorCreated>> Create(CreateAuthor input, CancellationToken ct)
        {
            if(await AuthorExists(input.FullName, ct)) 
                return Result.WithFailure<AuthorCreated>("author_fullname_conflicted", 409);

            
            var author = await _repository.Create(new Author {
                FullName = input.FullName,
                Nickname = input.Nickname,
            }, ct);

            return Result.WithSuccess(new AuthorCreated(author), 201);
        }

        private async Task<bool> AuthorExists(string fullname, CancellationToken ct)
        {
            var author = await _repository.FirstByFullName(fullname, ct);
            return author is not null;
        }
    }
}