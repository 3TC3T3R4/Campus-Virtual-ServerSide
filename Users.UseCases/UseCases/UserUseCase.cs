using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Commands;
using Users.Domain.Entities;
using Users.UseCases.Gateway;
using Users.UseCases.Gateway.Repositories;

namespace Users.UseCases.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<string> CreateUser(CreateUser user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetUserById(id);
        }
    }

}
