using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrainingServer.Models;

public class Training
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Force { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Area Area { get; set; } = null!;
    public Squadron? System { get; set; } = null!;
    public User Contact { get; set; } = null!;

}
