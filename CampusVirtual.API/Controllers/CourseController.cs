using AutoMapper;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace CampusVirtual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ICourseUseCase _courseUseCase;
        private readonly IMapper _mapper;


        public CourseController(ICourseUseCase courseUseCase, IMapper mapper)
        {
            _courseUseCase = courseUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Courses>> GetCoursesAsync()
        {
            return await _courseUseCase.GetCoursesAsync();
        }

    }
}
