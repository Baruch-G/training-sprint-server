using System.Linq;
using TrainingServer.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TrainingServer.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(IMongoCollection<User> user)
    {
        _userCollection = user;
    }

    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser)
    {
        await _userCollection.InsertOneAsync(newUser);
        return;
    }

    public async Task RemoveAsync(string id) =>
        await _userCollection.DeleteOneAsync(x => x.Id == id);
}
