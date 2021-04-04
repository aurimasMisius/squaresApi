using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo
{
    public abstract class BaseRepository<T>
    {
        protected readonly IMongoCollection<T> Collection;

        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            MongoCollectionSettings defaultSettings = new()
            {
                GuidRepresentation = GuidRepresentation.Standard,
                AssignIdOnInsert = false
            };

            Collection = database.GetCollection<T>(collectionName, defaultSettings);
        }
    }
}
