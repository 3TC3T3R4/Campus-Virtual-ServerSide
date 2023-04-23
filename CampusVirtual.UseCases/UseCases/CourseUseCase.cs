using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.UseCases.UseCases
{
    public class CourseUseCase : ICourseUseCase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseUseCase(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<Courses>> GetCoursesAsync()
        {
            return await _courseRepository.GetCoursesAsync();
        }
       

    }
}
