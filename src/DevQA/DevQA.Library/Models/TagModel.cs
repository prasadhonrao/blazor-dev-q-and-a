using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DevQA.Library.Models;

public class TagModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}
