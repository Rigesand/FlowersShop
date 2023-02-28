using System.Text.Json;
using FlowersShop.DAL.MongoDB.Configuration;
using FlowersShop.DAL.MongoDB.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using StackExchange.Redis;

namespace FlowersShop.DAL.MongoDB.Repositories;

public class FlowerRepository
{
    private readonly IMongoCollection<FlowerDb> _flowers;
    private readonly IDatabase _redis;

    public FlowerRepository(IConnectionMultiplexer muxer)
    {
        var mongoClient = new MongoClient(
            MongoDbSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            MongoDbSettings.DatabaseName);

        _flowers = mongoDatabase.GetCollection<FlowerDb>(
            MongoDbSettings.FlowersCollection);
        _redis = muxer.GetDatabase();
    }

    public async Task AddFlower(FlowerDb flower)
    {
        await _flowers.InsertOneAsync(flower);
    }

    public async Task<IEnumerable<FlowerDb>> GetAllFlowers()
    {
        return await _flowers.Find("{}").ToListAsync();
    }

    public async Task<FlowerDb> GetById(string name)
    {
        var flowerString = await _redis.StringGetAsync(name);
        if (!string.IsNullOrEmpty(flowerString))
        {
            var flower = JsonSerializer.Deserialize<FlowerDb>(flowerString);
            return flower;
        }

        var flowerDb = _flowers.Find(it => it.Name == name).Single();

        var flowerDbString = JsonSerializer.Serialize(flowerDb);
        var setTask = _redis.StringSetAsync(flowerDb.Name, flowerDbString);
        var expireTask = _redis.KeyExpireAsync(flowerDb.Name, TimeSpan.FromSeconds(10));
        await Task.WhenAll(setTask, expireTask);

        return flowerDb;
    }
}