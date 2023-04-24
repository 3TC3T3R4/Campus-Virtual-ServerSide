using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.UseCases.Gateway.Repositories
{
    public interface IUserRepository
    {
        Task<string> CreateUser(CreateUser user);
        Task<List<User>> GetUsers();
        Task<User> GetUserById(object id);
    }
}
