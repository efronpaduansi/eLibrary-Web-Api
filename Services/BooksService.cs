using PerpusApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PerpusApi.Services;

public class BooksService
{

    private readonly IMongoCollection<Book> _booksCollection;

    public BooksService(
        IOptions<PerpusApiDatabaseSettings> perpusApiDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            perpusApiDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            perpusApiDatabaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<Book>(
            perpusApiDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<Book>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book?> GetAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book newBook) =>
        await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Book updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);

}