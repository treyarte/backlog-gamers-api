﻿using backlog_gamers_api.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace backlog_gamers_api.Models;

public abstract class BaseMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonSerializer(typeof(CustomDateTimeOffsetSerializer))]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    
    [BsonSerializer(typeof(CustomDateTimeOffsetSerializer))]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}