using AutoMapper;
using CampusVirtual.Domain.Commands.Content;
using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Commands.LearningPath;
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
            CreateMap<InsertNewLearningPath, LearningPath>().ReverseMap();
            CreateMap<UpdateLearningPaths, LearningPath>().ReverseMap();
            #endregion

            #region Courses
            CreateMap<NewCourse, Courses>().ReverseMap();
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