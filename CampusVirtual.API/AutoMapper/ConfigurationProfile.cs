using AutoMapper;
using CampusVirtual.Domain.Commands.Registration;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            #region Registrations
            CreateMap<NewRegistrationCommand, Registration>().ReverseMap();
            #endregion

            #region Learning paths

            #endregion

            #region Courses

            #endregion

            #region Contents

            #endregion

            #region Deliveries

            #endregion
        }
    }
}