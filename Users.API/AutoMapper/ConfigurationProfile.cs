using AutoMapper;
using Users.Domain.Commands;
using Users.Domain.Entities;
using Users.Infrastructure.MongoAdapter.MongoEntities;

namespace Users.API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<UserMongo, User>().ReverseMap();
            CreateMap<CreateUser, UserMongo>().ReverseMap();
        }
    }
}