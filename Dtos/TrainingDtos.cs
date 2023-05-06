namespace TrainingServer.Dtos;
using TrainingServer.Models;
using MongoDB.Bson;

public record TrainingDto(string Force, DateTime StartDate, DateTime EndDate, Area Area, DateTime LastUpdateTime, DateTime CreationTime, ObjectId Id, User User);
public record CreateTrainingDto(string Force, DateTime StartDate, DateTime EndDate, string AreaId, string? SquadronId, string UserId);

public record CreateUserDto(string FirstName, string LastName, string Email, string PhoneNumber, string Img);




