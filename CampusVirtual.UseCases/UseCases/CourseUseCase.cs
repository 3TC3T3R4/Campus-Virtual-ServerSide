using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;

namespace CampusVirtual.UseCases.UseCases
{
    public class CourseUseCase : ICourseUseCase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseUseCase(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<NewCourse> CreateCourseAsync(Courses courses)
        {
            return await _courseRepository.CreateCourseAsync(courses);
        }

        public async Task<Courses> UpdateCourseAsync(UpdateCourse updateCourse)
        {
            return await _courseRepository.UpdateCourseAsync(updateCourse);
        }

        public async Task<Courses> GetCourseByIdAsync(Guid id)
        {
            return await _courseRepository.GetCourseByIdAsync(id);
        }

        public async Task<List<Courses>> GetCoursesByPathIdAsync(Guid id)
        {
            return await _courseRepository.GetCoursesByPathIdAsync(id);
        }

        public async Task<string> DeleteCourseAsync(string id)
        {
            return await _courseRepository.DeleteCourseAsync(id);
        }

        public async Task<Courses> UpdateDurationAsync(UpdateDuration updateDuration)
        {
            return await _courseRepository.UpdateDurationAsync(updateDuration);
        }

        public async Task<Courses> ConfigureToPathAsync(AssingToPath assingToPath)
        {
            return await _courseRepository.ConfigureToPathAsync(assingToPath);
        }

        public async Task<List<Courses>> GetActiveCoursesAsync()
        {
            return await _courseRepository.GetActiveCoursesAsync();
        }
    }
}
