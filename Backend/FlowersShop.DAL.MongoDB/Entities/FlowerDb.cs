using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlowersShop.DAL.MongoDB.Entities;

public class FlowerDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("Id")]
    public string Id { get; set; } = null!;
    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    public string Name { get; set; } = null!;
    [BsonElement("Price")]
    [JsonPropertyName("Price")]
    public decimal Price { get; set; }
}