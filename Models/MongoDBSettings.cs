namespace TrainingServer.Models;

public class MongoDBSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TrainingCollectionName { get; set; } = null!;
    public string AreaCollectionName { get; set; } = null!;
    public string SquadronCollectionName { get; set; } = null!;

}
