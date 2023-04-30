using System.Linq;
using TrainingServer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using TrainingServer.Dtos;

namespace TrainingServer.Services;

public class TrainingService
{
    private readonly IMongoCollection<Training> _trainingCollection;

    public TrainingService(IMongoCollection<Training> training)
    {
        _trainingCollection = training;
    }

    public async Task<List<Training>> GetAsync() =>
        await _trainingCollection.Find(_ => true).ToListAsync();

    // public async Task<List<string>> GetPrintKindsAsync()
    // {
    //     var fieldsBuilder = Builders<Training>.Projection;
    //     var fields = fieldsBuilder.Include(d => d.PrintKind);

    //     return await _trainingCollection
    //         .Distinct<string>("PrintKind", new BsonDocument())
    //         .ToListAsync(); //Find(_ => true).Project<Print>(fields).ToListAsync();
    // }

    public async Task<Training?> GetAsync(string id) =>
        await _trainingCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Training newTraining)
    {
        await _trainingCollection.InsertOneAsync(newTraining);

        return;
    }

    // public async Task UpdateAsync(string id, Training updatedPrint)
    // {
    //     updatedPrint.LastUpdateTime = DateTime.Now;
    //     await _trainingCollection
    //         .ReplaceOneAsync(x => x.Id == id, updatedPrint);
    // }

    public async Task RemoveAsync(string id) =>
        await _trainingCollection.DeleteOneAsync(x => x.Id == id);
}
