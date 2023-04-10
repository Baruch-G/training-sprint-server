using TrainingServer.Models;
using TrainingServer.Services;
using MongoDB.Driver;
using System.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;

namespace TrainingServer;

public static class DBConfigSetUp
{
    public static void ConfigureServices(IServiceCollection services, MongoDBSettings settings)
    {
        ConfigureMongoDb(services, settings);
        //services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
    }

    private static void ConfigureMongoDb(IServiceCollection services, MongoDBSettings settings)
    {
        var db = CreateMongoDatabase(settings);
        AddMongoDbService<TrainingService, Training>(settings.TrainingCollectionName);
        AddMongoDbService<AreaService, Area>(settings.AreaCollectionName);
        void AddMongoDbService<TService, TModel>(string collectionName)
        {
            services.AddSingleton(db.GetCollection<TModel>(collectionName));
            services.AddSingleton(typeof(TService));
        }
    }

    private static IMongoDatabase CreateMongoDatabase(MongoDBSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        return client.GetDatabase(settings.DatabaseName);
    }
}
