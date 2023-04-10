using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingServer.Models;

public class Area
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Color { get; set; } = null!;
    public string Name { get; set; } = null!;
}
