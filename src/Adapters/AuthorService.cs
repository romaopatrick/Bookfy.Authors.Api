using System.Globalization;
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
            if (await AuthorWithFullNameExists(input.FullName, ct))
                return Result.WithFailure<AuthorCreated>("author_fullname_conflicted", 409);

            var author = await _repository.Create(new Author
            {
                FullName = input.FullName.ToLower(),
                Nickname = input.Nickname?.ToLower() ?? "",
            }, ct);

            return Result.WithSuccess(new AuthorCreated(author), 201);
        }

        public async Task<Result<AuthorDeleted>> Delete(DeleteAuthor input, CancellationToken ct)
        {
            var author = await _repository.First(x => x.Id == input.Id, ct);
            if (author is null)
                return Result.WithFailure<AuthorDeleted>("author_not_found_with_id", 404);

            await _repository.Delete(input.Id, ct);

            return Result.WithSuccess(new AuthorDeleted(), 204);
        }

        public async Task<Result<Paginated<Author>>> Get(SearchAuthors input, CancellationToken ct)
        {
            var result = await _repository.Get(x => 
                x.FullName.StartsWith(input.SearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase) || 
                x.Nickname!.StartsWith(input.SearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase), 
                input.Skip ?? 0,
                input.Take ?? 10,
                ct);

            return Result.WithSuccess(result, 200);
        }

        public async Task<Result<Author>> GetById(GetAuthorById input, CancellationToken ct)
        {
            var author = await _repository.First(x => x.Id == input.Id, ct);

            return author is null
                ? Result.WithFailure<Author>("author_not_found_With_id", 404)
                : Result.WithSuccess(author, 200);
        }

        public async Task<Result<AuthorUpdated>> Update(UpdateAuthor input, CancellationToken ct)
        {
            var author = await _repository.First(x => x.Id == input.Id, ct);
            if (author is null)
                return Result.WithFailure<AuthorUpdated>("author_not_found_with_id", 404);

            if (!string.IsNullOrEmpty(input.FullName) &&
                await OtherAuthorWithFullNameExists(input.Id, input.FullName, ct))
                return Result.WithFailure<AuthorUpdated>("author_fullname_conflicted", 409);

            author.FullName = input.FullName ?? author.FullName;
            author.Nickname = input.Nickname;

            author = await _repository.Update(author, ct);

            return Result.WithSuccess(new AuthorUpdated(author), 200);
        }

        private async Task<bool> AuthorWithFullNameExists(string fullname, CancellationToken ct)
        {
            var author = await _repository.First(x => x.FullName.Equals(fullname, StringComparison.CurrentCultureIgnoreCase), ct);

            return author is not null;
        }

        private async Task<bool> OtherAuthorWithFullNameExists(Guid authorId, string fullname, CancellationToken ct)
        {
            var author = await _repository.First(x =>
                x.FullName
                    .Equals(fullname, StringComparison.CurrentCultureIgnoreCase)
                && x.Id != authorId, ct);

            return author is not null;
        }
    }
}