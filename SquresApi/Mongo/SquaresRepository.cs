using Models;
using MongoDB.Driver;

namespace Mongo
{
    public class SquaresRepository : BaseRepository<SquaresMetadata>
    {
        public SquaresRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }
    }
}
