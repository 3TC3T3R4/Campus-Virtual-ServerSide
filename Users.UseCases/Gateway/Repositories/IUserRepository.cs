using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.UseCases.Gateway.Repositories
{
    public interface IUserRepository
    {
        Task<string> CreateUser(CreateUser user);
        Task<List<User>> GetUsers();
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
    }
}
