using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.UseCases.Gateway
{
    public interface ICourseUseCase
    {
        Task<NewCourse> CreateProjectAsync(Courses courses);

        //Task<Courses> GetCourseByIdAsync(Guid id);
        Task<List<Courses>> GetCoursesAsync();
    }
}
