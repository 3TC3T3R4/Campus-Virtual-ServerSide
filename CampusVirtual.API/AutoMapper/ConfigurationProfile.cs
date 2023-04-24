using AutoMapper;
using CampusVirtual.Domain.Commands.Content;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
			#region Registrations

			#endregion

			#region Learning paths

			#endregion

			#region Courses

			#endregion

			#region Contents
			CreateMap<Content, CreateContentCommand>().ReverseMap();
			CreateMap<Content, UpdateContentCommand>().ReverseMap();
			#endregion

			#region Deliveries

			#endregion
		}
    }
}
