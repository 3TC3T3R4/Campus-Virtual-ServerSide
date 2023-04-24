using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.UseCases.Gateway
{
    public interface ICourseUseCase
    {

        Task<NewCourse> CreateCourseAsync(Courses courses);
        Task<Courses> GetCourseByIdAsync(Guid id);
        Task<List<Courses>> GetCoursesByPathIdAsync(Guid id);
        Task<Courses> UpdateCourseAsync(UpdateCourse updateCourse);
        Task<Courses> DeleteCourseAsync(string id);
        Task<Courses> UpdateDurationAsync(UpdateDuration updateDuration);


    }
}