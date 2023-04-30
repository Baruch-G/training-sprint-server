namespace TrainingServer.Dtos;
using TrainingServer.Models;
using MongoDB.Bson;

public record TrainingDto(string Force, DateTime StartDate, DateTime EndDate, Area Area, DateTime LastUpdateTime, DateTime CreationTime, ObjectId Id);
public record CreateTrainingDto(string Force, DateTime StartDate, DateTime EndDate, string AreaId, string? SquadronId);

public record CreateSquadronDto(string Name, string Img);



