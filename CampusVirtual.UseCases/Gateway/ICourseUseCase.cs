using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.UseCases.Gateway
{
    public interface ICourseUseCase
    {

        Task<NewCourse> CreateCourseAsync(Courses courses);
        Task<Courses> GetCourseByIdAsync(Guid id);
        Task<List<Courses>> GetCoursesByPathIdAsync(Guid id);
        Task<List<Courses>> GetActiveCoursesAsync();
        Task<Courses> UpdateCourseAsync(UpdateCourse updateCourse);
        Task<string> DeleteCourseAsync(string id);
        Task<Courses> UpdateDurationAsync(UpdateDuration updateDuration);
        Task<Courses> ConfigureToPathAsync(AssingToPath assingToPath);
    }
}