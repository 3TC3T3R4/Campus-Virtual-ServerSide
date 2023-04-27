using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.Domain.Commands;
using Users.Domain.Entities;
using Users.UseCases.Gateway;

namespace Users.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IUserUseCase _userUseCase;
        private readonly IMapper _mapper;

        public UserController(IUserUseCase userUseCase, IMapper mapper)
        {
            _userUseCase = userUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<string> CreateUser(CreateUser user)
        {
            return await _userUseCase.CreateUser(user);
        }

        [HttpGet("")]
        public async Task<List<User>> GetUsers()
        {
            return await _userUseCase.GetUsers();
        }

        [HttpGet("ID")]
        public async Task<User> GetUserById(string Id)
        {
            return await _userUseCase.GetUserById(Id);
        }

        [HttpGet("Email")]
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userUseCase.GetUserByEmail(email);
        }
    }
}