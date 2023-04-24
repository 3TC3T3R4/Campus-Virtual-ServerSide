using AutoMapper;
using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Domain.Commands.Content;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Domain.Commands.Registration;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
			#region Registrations

			#endregion
            #region Registrations
            CreateMap<NewRegistrationCommand, Registration>().ReverseMap();
            #endregion

			#region Learning paths

			#endregion

            CreateMap<NewCourse, Courses>().ReverseMap();
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