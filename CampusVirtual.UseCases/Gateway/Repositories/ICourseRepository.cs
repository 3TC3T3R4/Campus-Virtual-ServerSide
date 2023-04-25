﻿using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.UseCases.Gateway.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Courses>> GetCoursesByPathIdAsync(Guid id);
        Task<List<Courses>> GetActiveCoursesAsync();
        Task<NewCourse> CreateCourseAsync(Courses courses);
        Task<Courses> UpdateCourseAsync(UpdateCourse updateCourse);
        Task<Courses> GetCourseByIdAsync(Guid id);
        Task<string> DeleteCourseAsync(string id);
        Task<Courses> UpdateDurationAsync(UpdateDuration updateDuration);
        Task<Courses> ConfigureToPathAsync(AssingToPath assingToPath);
    }
}