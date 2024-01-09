
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace backlog_gamers_api.Extensions;

/// <summary>
/// This is for serializing off the mongo date time for LoanLeads.
/// The LoanLeads collection/db was setup by another developer and setup captureTime to use Mongo's timestamp.
/// They used this function to serialize/deserialize that timestamp.
/// </summary>
public class DateTimeOffsetSerializer : StructSerializerBase<DateTimeOffset>, IRepresentationConfigurable<DateTimeOffsetSerializer>
    {
        private BsonType _representation;
        private string StringSerializationFormat = "YYYY-MM-ddTHH:mm:ss.FFFFFFK";

        public DateTimeOffsetSerializer() : this(BsonType.DateTime)
        {
        }

        public DateTimeOffsetSerializer(BsonType representation)
        {
            switch (representation)
            {
                case BsonType.String:
                case BsonType.DateTime:
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid representation for {1}", representation, this.GetType().Name));
            }

            _representation = representation;
        }

        public BsonType Representation => _representation;

        public override DateTimeOffset Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            //long ticks;
            //TimeSpan offset;

            BsonType bsonType = bsonReader.GetCurrentBsonType();
            switch (bsonType)
            {
                case BsonType.String:
                    var stringValue = bsonReader.ReadString();
                    return DateTimeOffset.ParseExact
                        (stringValue, StringSerializationFormat, DateTimeFormatInfo.InvariantInfo);

                case BsonType.DateTime:
                    var dateTimeValue = bsonReader.ReadDateTime();
                    return DateTimeOffset.FromUnixTimeMilliseconds(dateTimeValue);

                default:
                    throw CreateCannotDeserializeFromBsonTypeException(bsonType);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTimeOffset value)
        {
            var bsonWriter = context.Writer;

            switch (_representation)
            {
                case BsonType.String:
                    bsonWriter.WriteString(value.ToString
                          (StringSerializationFormat, DateTimeFormatInfo.InvariantInfo));
                    break;

                case BsonType.DateTime:
                    bsonWriter.WriteDateTime(value.ToUnixTimeMilliseconds());
                    break;

                default:
                    var message = string.Format("'{0}' is not a valid DateTimeOffset representation.", _representation);
                    throw new BsonSerializationException(message);
            }
        }

        public DateTimeOffsetSerializer WithRepresentation(BsonType representation)
        {
            if (representation == _representation)
            {
                return this;
            }
            return new DateTimeOffsetSerializer(representation);
        }

        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
        {
            return WithRepresentation(representation);
        }
    }