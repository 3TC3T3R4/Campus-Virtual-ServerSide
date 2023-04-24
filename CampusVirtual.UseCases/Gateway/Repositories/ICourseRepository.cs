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
        Task<List<Courses>> GetCoursesAsync();
        Task<NewCourse> CreateCourseAsync(Courses courses);
        Task<UpdateCourse> UpdateCourseAsync(Courses courses);
        Task<Courses> GetCourseByIdAsync(Guid id);
        Task<Courses> DeleteCourseAsync(string id);
    }
}
