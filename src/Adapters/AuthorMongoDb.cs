using System.Linq.Expressions;
using Bookfy.Authors.Api.Boundaries;
using Bookfy.Authors.Api.Domain;
using Bookfy.Authors.Api.Ports;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bookfy.Authors.Api.Adapters
{
    public class AuthorMongoDb(IMongoClient mongoClient, IOptions<MongoDbSettings> dbSettings) : IAuthorRepository
    {
        private readonly IMongoCollection<Author> _collection =
            mongoClient
                .GetDatabase(dbSettings.Value.Database)
                .GetCollection<Author>(nameof(Author));

        public async Task<Author> Create(Author author, CancellationToken ct)
        {
            author.Id = Guid.NewGuid();
            author.CreatedAt = DateTime.UtcNow;
            await _collection
                .InsertOneAsync(author, 
                    cancellationToken: ct);

            return author;
        }

        public async Task Delete(Guid id, CancellationToken ct) 
            => await _collection.DeleteOneAsync(x => x.Id == id, ct);

        public Task<Author> First(Expression<Func<Author, bool>> predicate, CancellationToken ct)
            => _collection.Find(predicate).FirstOrDefaultAsync(ct);

        public async Task<Paginated<Author>> Get(Expression<Func<Author, bool>> filter, long skip, long take, CancellationToken ct)
        {
            var filtered = _collection.Find(filter);
            var total = await filtered.CountDocumentsAsync(ct); 
            var results = await filtered.Skip((int)skip)
                .Limit((int)take)
                .SortBy(x => x.FullName)
                .ThenBy(x => x.Nickname)
                .ToListAsync(cancellationToken: ct);
                
            return new Paginated<Author> 
            {
                Total = total,
                Results = results,
            };
        }
        

        public async Task<Author> Update(Author author, CancellationToken ct)
        {
            author.UpdatedAt = DateTime.UtcNow;
            await _collection
                .ReplaceOneAsync(
                    x => x.Id == author.Id,
                    author,
                    cancellationToken: ct);

            return author;
        }

    }
}