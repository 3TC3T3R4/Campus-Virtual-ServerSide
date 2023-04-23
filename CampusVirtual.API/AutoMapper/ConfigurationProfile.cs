using AutoMapper;
using CampusVirtual.Domain.Commands.LearningPath;
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

            CreateMap<InsertNewLearningPath, LearningPath>().ReverseMap();

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
