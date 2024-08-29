using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backlog_gamers_api.Models;

/// <summary>
/// Class for representing a mongo ID Object 
/// </summary>
/// <remarks>
/// There is a bug in the mongo cSharp driver where
/// you cannot serialize a list of object ids. You can
/// get around this by creating a seperate obj
/// </remarks>
public class MongoIdObject
{
    public MongoIdObject(string id)
    {
        Id = id;
    }
    
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string Id { get; set; }
}