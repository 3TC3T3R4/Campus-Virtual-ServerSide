
using Ardalis.GuardClauses;
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
            Guard.Against.Null(user, nameof(user), "User is null");
            Guard.Against.Null(user.uidUser, nameof(user.uidUser), "uidUser is null");
            Guard.Against.Null(user.email, nameof(user.email), "email is null");
            Guard.Against.Null(user.password, nameof(user.password), "password is null");
            Guard.Against.Null(user.role, nameof(user.role), "role is null");
            
            if(user.email.Length < 4)
            {
                return "Incorrect format";
            }
			if (user.password.Length < 7)
			{
				return "Incorrect format";
			}

			// Verificar si el uidUser ya existe
			var existingUser = await GetUserById(user.uidUser);
            if (existingUser != null)
            {
                return "uidUser already exists";
            }

            await _collection.InsertOneAsync(_mapper.Map<UserMongo>(user));
            return "User Created";
        }

        public async Task<User> GetUserById(string uid)
        {
            Guard.Against.NullOrEmpty(uid, nameof(uid), "Uid is null or empty");

            var user = await _collection.FindAsync(x => x.uidUser == uid.ToString());
            if (user == null)
            {
                throw new ArgumentException($"User with uid {uid} does not exist");
            }

            var userList = user.ToEnumerable().Select(x => _mapper.Map<User>(x)).ToList();
            return userList.FirstOrDefault();
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _collection.FindAsync(Builders<UserMongo>.Filter.Empty);
            var userList = users.ToEnumerable().Select(x => _mapper.Map<User>(x)).ToList();
            if (userList.Count == 0)
            {
                throw new Exception("No users found.");
            }
            return userList;
        }
    }
}
