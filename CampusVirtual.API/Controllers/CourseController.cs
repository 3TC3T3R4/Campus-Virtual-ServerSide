using AutoMapper;
using CampusVirtual.Domain.Commands.Courses;
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

        [HttpPost]
        public async Task<NewCourse> CreateCourseAsync([FromBody] NewCourse newCourse)
        {
            return await _courseUseCase.CreateCourseAsync(_mapper.Map<Courses>(newCourse));
        }

        [HttpGet]
        [Route("GetCoursesByPathId")]
        public async Task<List<Courses>> GetCoursesByPathIdAsync([FromQuery] Guid id)
        {
            return await _courseUseCase.GetCoursesByPathIdAsync(id);
        }       

        [HttpGet]
        [Route("GetCourseById")]

        public async Task<Courses> GetCourseByIdAsync([FromQuery] Guid id)
        {
            return await _courseUseCase.GetCourseByIdAsync(id);
        }

        [HttpPut]
        public async Task<Courses> UpdateCourseAsync(UpdateCourse updateCourse)
        {
            return await _courseUseCase.UpdateCourseAsync(updateCourse);
        }

        [HttpDelete("{id}")]
        public async Task<Courses> DeleteCourseAsync(string id)
        {
            return await _courseUseCase.DeleteCourseAsync(id);
        }

        [HttpPut]
        [Route("UpdateDuration")]
        public async Task<Courses> UpdateDurationAsync(UpdateDuration updateDuration)
        {
            return await _courseUseCase.UpdateDurationAsync(updateDuration);
        }        

        [HttpPatch]
        [Route("AssingToPath")]

        public async Task<Courses> AssingToPathAsync(AssingToPath assingToPath)
        {
            return await _courseUseCase.AssingToPathAsync(assingToPath);
        }


    }
}
