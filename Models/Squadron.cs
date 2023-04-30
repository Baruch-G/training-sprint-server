using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace TrainingServer.Models;

public class Squadron
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Img  { get; set; } = null!;
}
