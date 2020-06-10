using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit.Abstractions;

namespace Samples.Tests
{
    public abstract class PrimerTestFixture
    {
        protected static IMongoClient __client;
        protected static IMongoDatabase __database;

        private static readonly Lazy<List<BsonDocument>> __lazyDataset = new Lazy<List<BsonDocument>>(LoadDataSetFromResource);
        
        private static Lazy<bool> __lazyOneTimeSetup = new Lazy<bool>(OneTimeSetup);
        private static bool __reloadCollection = true;

        protected readonly ITestOutputHelper Output;

        protected PrimerTestFixture(ITestOutputHelper output, bool reloadCollection = true)
        {
            Output = output;

            var _ = __lazyOneTimeSetup.Value;

            __reloadCollection = reloadCollection;

            if (__reloadCollection)
            {
                LoadCollection();                
                __reloadCollection = false;
            }
        }

        private static bool OneTimeSetup()
        {
            var connectionString = CoreTestConfiguration.ConnectionString.ToString();
            __client = new MongoClient(connectionString);
            __database = __client.GetDatabase("test");
            LoadDataSetFromResource();
            return true;
        }

        // protected methods
        protected void AltersCollection()
        {
            __reloadCollection = true;
        }

        // helper methods
        private void LoadCollection()
        {
            __database.DropCollection("restaurants");

            var collection = __database.GetCollection<BsonDocument>("restaurants");
            collection.InsertMany(__lazyDataset.Value);
        }

        private static List<BsonDocument> LoadDataSetFromResource()
        {
            var dataset = new List<BsonDocument>();

            var assembly = typeof(PrimerTestFixture).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("Samples.Tests.dataset.json"))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var document = BsonDocument.Parse(line);
                    dataset.Add(document);
                }
            }
            return dataset;
        }
    }
}
