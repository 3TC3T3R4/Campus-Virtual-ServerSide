using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Users.Infrastructure.MongoAdapter.MongoEntities
{
    public class UserMongo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public Object Id { get; set; }
        public string uidUser { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role { get; set; }
        public int stateUser { get; set; } = 1;
    }
}
