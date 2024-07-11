using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PerpusApi.Models;

public class Member
{
    //Define member fields
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")] 
    public string MemberName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;
    
    public bool IsActive { get; set; }
}