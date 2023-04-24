using MongoDB.Driver;
using Users.Infrastructure.MongoAdapter.MongoEntities;

//using Users.Infrastructure.Entities;

namespace Users.Infrastructure.MongoAdapter.Interfaces
{
    public interface IContext
    {
        public IMongoCollection<UserMongo> Users { get; }
    }
}
