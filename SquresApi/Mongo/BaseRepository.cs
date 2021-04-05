using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Mongo
{
    public abstract class BaseRepository<T> where T : BaseDocument
    {
        protected readonly IMongoCollection<T> Collection;

        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            MongoCollectionSettings defaultSettings = new()
            {
                AssignIdOnInsert = false
            };

            Collection = database.GetCollection<T>(collectionName, defaultSettings);
        }

        public Task<T> Find(Guid id)
        {
            FilterDefinition<T> filter = GetFilterById(id);
            return Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> Create(T entity)
        {
            await Collection.InsertOneAsync(entity).ConfigureAwait(false);
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            await Collection.ReplaceOneAsync(filter, entity).ConfigureAwait(false);
            return entity;
        }

        public async Task Delete(Guid id)
        {
            FilterDefinition<T> filter = GetFilterById(id);
            await Collection.DeleteOneAsync(filter).ConfigureAwait(false);
        }

        protected FilterDefinition<T> GetFilterById(Guid id)
        {
            return Builders<T>.Filter.Eq(x => x.Id, id);
        }

        protected static FindOneAndUpdateOptions<T, T> CreateUpdateOneOptionsWithBeforeDocument()
        {
            return new()
            {
                ReturnDocument = ReturnDocument.Before,
                IsUpsert = false
            };
        }

        protected static UpdateOptions CreateDefaultUpdateOneOptions()
        {
            return new()
            {
                IsUpsert = false
            };
        }
    }
}
