using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using Xunit;
using Xunit.Abstractions;

namespace Samples.Tests
{
    public class InsertPrimer : PrimerTestFixture
    {
        public InsertPrimer(ITestOutputHelper output): base(output)
        {

        }

        [Fact]
        public async Task InsertADocument()
        {
            AltersCollection();

            // @begin: insert-a-document
            // @code: start
            var document = new BsonDocument
            {
                { "address" , new BsonDocument
                    {
                        { "street", "2 Avenue" },
                        { "zipcode", "10075" },
                        { "building", "1480" },
                        { "coord", new BsonArray { 73.9557413, 40.7720266 } }
                    }
                },
                { "borough", "Manhattan" },
                { "cuisine", "Italian" },
                { "grades", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "A" },
                            { "score", 11 }
                        },
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "B" },
                            { "score", 17 }
                        }
                    }
                },
                { "name", "Vella" },
                { "restaurant_id", "41704620" }
            };

            var collection = __database.GetCollection<BsonDocument>("restaurants");
            await collection.InsertOneAsync(document);
            // @code: end

            // @post: The method does not return a result
            // @end: insert-a-document
        }
    }
}
