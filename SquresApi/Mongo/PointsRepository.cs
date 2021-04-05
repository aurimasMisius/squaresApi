using Models;
using MongoDB.Driver;

namespace Mongo
{
    public class PointsRepository : BaseRepository<PointsMetadata>
    {
        public PointsRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }
    }
}
