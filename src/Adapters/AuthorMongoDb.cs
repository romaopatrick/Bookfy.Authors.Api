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


        public async Task<Author?> FirstByFullName(string fullname, CancellationToken ct)
        {
            var result = await _collection
                .Find(Builders<Author>.Filter.Eq(x => x.FullName, fullname))
                .FirstOrDefaultAsync(ct);

            return result;
        }

        public async Task<Author> Create(Author author, CancellationToken ct)
        {
            await _collection
                .InsertOneAsync(author, new(), ct);


            return author;
        }
    }
}