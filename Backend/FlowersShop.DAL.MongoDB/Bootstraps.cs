using FlowersShop.DAL.MongoDB.Entities;
using FlowersShop.DAL.MongoDB.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using StackExchange.Redis;

namespace FlowersShop.DAL.MongoDB;

public static class Bootstraps
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddSingleton<FlowerRepository>();
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
        return services;
    }
}