using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.UseCases.Gateway.Repositories
{
    public interface ICourseRepository
    {
        Task<Courses> GetCoursesByPathIdAsync(Guid id);
        Task<NewCourse> CreateCourseAsync(Courses courses);
        Task<Courses> UpdateCourseAsync(UpdateCourse updateCourse);
        Task<Courses> GetCourseByIdAsync(Guid id);
        Task<Courses> DeleteCourseAsync(string id);
        Task<Courses> UpdateDurationAsync(UpdateDuration updateDuration);
    }
}
