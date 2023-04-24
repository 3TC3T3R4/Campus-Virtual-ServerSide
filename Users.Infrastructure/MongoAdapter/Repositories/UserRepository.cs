using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Users.Domain.Commands;
using Users.Domain.Entities;
using Users.Infrastructure.MongoAdapter.Interfaces;
using Users.Infrastructure.MongoAdapter.MongoEntities;
using Users.UseCases.Gateway.Repositories;

namespace Users.Infrastructure.MongoAdapter.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserMongo> _collection;
        private readonly IMapper _mapper;

        public UserRepository(IContext context, IMapper mapper)
        {
            _collection = context.Users;
            _mapper = mapper;
        }

        public async Task<string> CreateUser(CreateUser user)
        {
            await _collection.InsertOneAsync(_mapper.Map<UserMongo>(user));
            return "User Created";
        }

        public async Task<User> GetUserById(object id)
        {
            var user = await _collection.FindAsync(x => x.Id == id.ToString());
            var userList = user.ToEnumerable().Select(x => _mapper.Map<User>(x)).ToList();
            return userList.FirstOrDefault();
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _collection.FindAsync(Builders<UserMongo>.Filter.Empty);
            var userList = users.ToEnumerable().Select(x => _mapper.Map<User>(x)).ToList();
            return userList;
        }
    }
}
