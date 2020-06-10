using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Samples.Serializers
{
    /// <summary>
    /// https://www.codeproject.com/articles/798004/saving-prices-as-decimal-in-mongodb
    /// </summary>
    [BsonSerializer(typeof(MongoMoneySerializer))]
    public class MongoMoneySerializer : IBsonSerializer
    {
        public Type ValueType => throw new NotImplementedException();

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var dbData = context.Reader.ReadInt32();
            return (decimal)dbData / (decimal)100;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var realValue = (decimal)value;
            context.Writer.WriteInt32(Convert.ToInt32(realValue * 100));
        }
    }

    public class Product
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [BsonSerializer(typeof(MongoMoneySerializer))]
        public decimal Price { get; set; }

        [BsonSerializer(typeof(MongoMoneySerializer))]
        public decimal MemberPrice { get; set; }

        public int Quantity { get; set; }
    }
}
