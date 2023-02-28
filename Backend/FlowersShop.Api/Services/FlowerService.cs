using FlowersShop.Api.Models;
using FlowersShop.DAL.MongoDB.Entities;
using FlowersShop.DAL.MongoDB.Repositories;

namespace FlowersShop.Api.Services;

public class FlowerService
{
    private readonly FlowerRepository _flowerRepository;

    public FlowerService(FlowerRepository flowerRepository)
    {
        _flowerRepository = flowerRepository;
    }

    public async Task AddFlower(Flower flower)
    {
        var flowerDb = new FlowerDb()
        {
            Name = flower.Name,
            Price = flower.Price
        };
        await _flowerRepository.AddFlower(flowerDb);
    }

    public async Task<IEnumerable<Flower>> GetAllFlowers()
    {
        var flowersDb = await _flowerRepository.GetAllFlowers();
        var flowers = new List<Flower>();
        foreach (var flower in flowersDb)
        {
            flowers.Add(new Flower()
            {
                Name = flower.Name,
                Price = flower.Price
            });
        }

        return flowers;
    }

    public async Task<Flower> GetById(string name)
    {
        var flowerDb= await _flowerRepository.GetById(name);
        return new Flower()
        {
            Name = flowerDb.Name,
            Price = flowerDb.Price,
        };
    }
}