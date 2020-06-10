using MongoDB.Driver;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MongoDbSample
{
    public interface ISetting { }

    public interface IEntity { }

    public class MongoSetting : ISetting
    {
        [JsonPropertyName("connectionString")]
        public string ConnectionString { get; set; }
        [JsonPropertyName("database")]
        public string Database { get; set; }
    }

    public static class DbSetExtensions
    {
        public static async Task<T> GetById<T>(this IMongoCollection<T> collection, string id) where T : IEntity
        {
            var docs = await collection.FindAsync(Builders<T>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(id)));
            return docs.SingleOrDefault();
        }

        public static async Task<IEnumerable<T>> GetAll<T>(this IMongoCollection<T> collection) where T : IEntity
        {
            var all = await collection.FindAsync(Builders<T>.Filter.Empty);
            return all.ToList();
        }
    }
}
