using backlog_gamers_api.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace backlog_gamers_api.Models;

/// <summary>
/// Base class for objects that get stored in mongo
/// </summary>
public abstract class BaseMongoModel
{
    /// <summary>
    /// Identifier for an object stored as an objectId in mongo
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    /// <summary>
    /// When the object was created at
    /// </summary>
    [BsonElement("createdAt")]
    [BsonSerializer(typeof(CustomDateTimeOffsetSerializer))]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    
    /// <summary>
    /// When the object was last updated
    /// </summary>
    [BsonElement("updatedAt")]
    [BsonSerializer(typeof(CustomDateTimeOffsetSerializer))]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}