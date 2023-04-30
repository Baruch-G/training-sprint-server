using System.Linq;
using TrainingServer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using TrainingServer.Dtos;

namespace TrainingServer.Services;

public class SquadronService
{
    private readonly IMongoCollection<Squadron> _squadropnCollection;

    public SquadronService(IMongoCollection<Squadron> squadron)
    {
        _squadropnCollection = squadron;
    }

    public async Task<List<Squadron>> GetAsync() =>
        await _squadropnCollection.Find(_ => true).ToListAsync();

    public async Task<Squadron?> GetAsync(string id) =>
        await _squadropnCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Squadron newSquadron)
    {
        await _squadropnCollection.InsertOneAsync(newSquadron);

        return;
    }

    // public async Task UpdateAsync(string id, Training updatedPrint)
    // {
    //     updatedPrint.LastUpdateTime = DateTime.Now;
    //     await _trainingCollection
    //         .ReplaceOneAsync(x => x.Id == id, updatedPrint);
    // }

    public async Task RemoveAsync(string id) =>
        await _squadropnCollection.DeleteOneAsync(x => x.Id == id);
}
