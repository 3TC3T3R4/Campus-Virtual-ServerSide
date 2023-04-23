using AutoMapper;
using CampusVirtual.Domain.Commands.Courses;
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

            CreateMap<NewCourse, Courses>().ReverseMap();

            #region Contents

            #endregion

            #region Deliveries

            #endregion
        }
    }
}
