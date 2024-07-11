using PerpusApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PerpusApi.Services;

public class MembersService
{

    private readonly IMongoCollection<Member> _membersCollection;

    public MembersService(IOptions<PerpusApiDatabaseSettings> perpusApiDatabaseSettings)
    {
        //call connection URI
        var mongoClient = new MongoClient(perpusApiDatabaseSettings.Value.ConnectionString);
        
        //call database name
        var mongoDatabase = mongoClient.GetDatabase(perpusApiDatabaseSettings.Value.DatabaseName);
        
        //call collection name
        _membersCollection = mongoDatabase.GetCollection<Member>(perpusApiDatabaseSettings.Value.MembersCollectionName);
    }
    
    //Get all members data
    public async Task<List<Member>> GetAsync() =>
        await _membersCollection.Find(_ => true).ToListAsync();
    
    //Get member data by Id (show details)
    public async Task<Member?> GetAsync(string id) =>
        await _membersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    //Create new member
    public async Task CreateAsync(Member newMember) =>
        await _membersCollection.InsertOneAsync(newMember);

    //Update member data by Id
    public async Task UpdateAsync(string id, Member updatedMember) =>
        await _membersCollection.ReplaceOneAsync(x => x.Id == id, updatedMember);
    
    //Remove (Delete) member data by Id
    public async Task RemoveAsync(string id) =>
        await _membersCollection.DeleteOneAsync(x => x.Id == id);
}   