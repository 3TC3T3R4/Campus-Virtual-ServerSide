using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRepository
{
    public class CourseRepositoryTest
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;
        public CourseRepositoryTest()
        {
            _mockCourseRepository = new();
        }

        [Fact]

        public async Task CreateCourseAsync()
        {
            // Arrange
            var newCourse = new Courses
            {
                CourseID = Guid.NewGuid(),
                PathID = Guid.NewGuid(),
                Title = "Test Course",
                Description = "Test Description",
                Duration = 10,
                StateCourse = 60,
            };

            var courseCreated = new NewCourse
            {
                Title = "Test Course",
                Description = "Test Description",
            };

            _mockCourseRepository.Setup(x => x.CreateCourseAsync(newCourse)).ReturnsAsync(courseCreated);

            //Act
            var result = await _mockCourseRepository.Object.CreateCourseAsync(newCourse);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(courseCreated, result);
        }

        [Fact]
        public async Task UpdateCourseAsync()
        {
            //arrange
            var courseToUpdate = new UpdateCourse
            {
                CourseID = Guid.NewGuid(),
                Title = "Test Course",
                Description = "Test Description",
                StateCourse = 60,
            };

            var courseUpdated = new Courses
            {
                CourseID = Guid.NewGuid(),
                Title = "Test Course",
                Description = "Test Description",
                StateCourse = 60,
            };


            _mockCourseRepository.Setup(x => x.UpdateCourseAsync(courseToUpdate)).ReturnsAsync(courseUpdated);

            //act

            var result = await _mockCourseRepository.Object.UpdateCourseAsync(courseToUpdate);

            //assert
            Assert.NotNull(result);
            Assert.Equal(courseUpdated, result);
        }

        [Fact]

        public async Task DeleteCourseAsync()
        {
            //arrange
            var courseToDelete = new Courses
            {
                CourseID = Guid.NewGuid(),
            };

            _mockCourseRepository.Setup(x => x.DeleteCourseAsync("test-course-123")).ReturnsAsync("Delete Successful for ID: test-course-123");

            // act
            var result = await _mockCourseRepository.Object.DeleteCourseAsync("test-course-123");

            // assert
            Assert.NotNull(result);
            Assert.Equal("Delete Successful for ID: test-course-123", result);

        }

        [Fact]

        public async Task GetActiveCoursesAsync()
        {
            //arrange
            var activeCourses = new List<Courses>
            {
                new Courses
                {
                    CourseID = Guid.NewGuid(),
                    PathID = Guid.NewGuid(),
                    Title = "Test Course",
                    Description = "Test Description",
                    Duration = 10,
                    StateCourse = 60,
                },
                new Courses
                {
                    CourseID = Guid.NewGuid(),
                    PathID = Guid.NewGuid(),
                    Title = "Test Course",
                    Description = "Test Description",
                    Duration = 10,
                    StateCourse = 60,
                },
                new Courses
                {
                    CourseID = Guid.NewGuid(),
                    PathID = Guid.NewGuid(),
                    Title = "Test Course",
                    Description = "Test Description",
                    Duration = 10,
                    StateCourse = 60,
                }
            };

            _mockCourseRepository.Setup(x => x.GetActiveCoursesAsync()).ReturnsAsync(activeCourses);

            // act
            var result = await _mockCourseRepository.Object.GetActiveCoursesAsync();

            // assert
            Assert.NotNull(result);
            Assert.Equal(activeCourses, result);
        }

        [Fact]

        public async Task UpdateDurationAsync()

        {
            //arrange
            var durationToUpdate = new UpdateDuration
            {
                CourseID = Guid.NewGuid(),
                Duration = 10,
            };           

            var durationUpdated = new Courses
            {
                CourseID = Guid.NewGuid(),
                Duration = 10,
            };

            _mockCourseRepository.Setup(x => x.UpdateDurationAsync(durationToUpdate)).ReturnsAsync(durationUpdated);

            //act

            var result = await _mockCourseRepository.Object.UpdateDurationAsync(durationToUpdate);

            //assert
            Assert.NotNull(result);
            Assert.Equal(durationUpdated, result);
        }

        [Fact]

        public async Task ConfigureToPathAsync()
        {
            //arrange
                var courseToConfigure = new AssingToPath
                {
                    CourseID = Guid.NewGuid(),
                    PathID = Guid.NewGuid(),
                };
    
                var courseConfigured = new Courses
                {
                    CourseID = Guid.NewGuid(),
                    PathID = Guid.NewGuid(),
                };
    
                _mockCourseRepository.Setup(x => x.ConfigureToPathAsync(courseToConfigure)).ReturnsAsync(courseConfigured);
    
                //act
    
                var result = await _mockCourseRepository.Object.ConfigureToPathAsync(courseToConfigure);
    
                //assert
                Assert.NotNull(result);
                Assert.Equal(courseConfigured, result);
        }

    }
}

           
 