using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrainingServer.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime JoinDate { get; set; }
    public string Img { get; set; } = null!;
}
