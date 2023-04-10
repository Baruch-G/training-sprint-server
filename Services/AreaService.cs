using System.Linq;
using TrainingServer.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TrainingServer.Services;

public class AreaService
{
    private readonly IMongoCollection<Area> _areaCollection;

    public AreaService(IMongoCollection<Area> area)
    {
        _areaCollection = area;
    }

    public async Task<List<Area>> GetAsync() =>
        await _areaCollection.Find(_ => true).ToListAsync();

    // public async Task<List<string>> GetPrintKindsAsync()
    // {
    //     var fieldsBuilder = Builders<Training>.Projection;
    //     var fields = fieldsBuilder.Include(d => d.PrintKind);

    //     return await _trainingCollection
    //         .Distinct<string>("PrintKind", new BsonDocument())
    //         .ToListAsync(); //Find(_ => true).Project<Print>(fields).ToListAsync();
    // }

    public async Task<Area?> GetAsync(string id) =>
        await _areaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Area newArea)
    {
        await _areaCollection.InsertOneAsync(newArea);
        return;
    }

    // public async Task UpdateAsync(string id, Training updatedPrint)
    // {
    //     updatedPrint.LastUpdateTime = DateTime.Now;
    //     await _trainingCollection
    //         .ReplaceOneAsync(x => x.Id == id, updatedPrint);
    // }

    public async Task RemoveAsync(string id) =>
        await _areaCollection.DeleteOneAsync(x => x.Id == id);
}
