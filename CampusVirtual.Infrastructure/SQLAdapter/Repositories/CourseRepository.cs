using AutoMapper;
using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;

namespace CampusVirtual.Infrastructure.SQLAdapter.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;

        private readonly string _tableNameCourses = "Courses";

        private readonly IMapper _mapper;

        public CourseRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<List<Courses>> GetCoursesAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCourses}";
            var result = await connection.QueryAsync<Courses>(sqlQuery);
            connection.Close();
            return result.ToList();
        }

        public async Task<NewCourse> CreateCourseAsync(Courses courses)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            Courses.SetDetailsCoursesEntity(courses);

            var courseToCreate = new Courses
            {
                PathID = courses.PathID,
                Title = courses.Title,
                Description = courses.Description,
                Duration = courses.Duration,
                StateCourse = courses.StateCourse
            };

            string sqlQuery = $"INSERT INTO {_tableNameCourses} (PathID, Title, Description, Duration, StateCourse)" +
                $"VALUES (@PathID, @Title, @Description, @Duration, @StateCourse)";

            var result = await connection.ExecuteScalarAsync(sqlQuery, courseToCreate);
            connection.Close();
            return _mapper.Map<NewCourse>(courseToCreate);
        }

        public async Task<Courses> GetCourseByIdAsync(Guid id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCourses} WHERE CourseID = @CourseID" +
                                $"AND StateCourse <> 3";
            var result = await connection.QueryFirstOrDefaultAsync<Courses>(sqlQuery, new { CourseID = id });
            connection.Close();
            return result;
        }

        public async Task<Courses> DeleteCourseAsync(string id)
        {

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            string sqlQuery = $"UPDATE {_tableNameCourses} SET StateCourse = 3 WHERE CourseID = @CourseID";

            var result = await connection.ExecuteAsync(sqlQuery, new { CourseID = id });

            connection.Close();

            if (result == 0)
            {
                return null;
            }

            return await GetCourseByIdAsync(Guid.Parse(id));

        }

        public async Task<Courses> UpdateDurationAsync(UpdateDuration updateDuration)
        {

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var durationToUpdate = await GetCourseByIdAsync(updateDuration.PathID);
            durationToUpdate.Duration += updateDuration.Duration;

            string sqlQuery = $"UPDATE {_tableNameCourses} SET Duration = @Duration WHERE CourseID = @CourseID";

            var result = await connection.ExecuteScalarAsync(sqlQuery, durationToUpdate);

            connection.Close();

            return _mapper.Map<Courses>(durationToUpdate);

        }

        public async Task<List<Courses>> GetCoursesByPathIdAsync(Guid id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCourses} WHERE PathID = @PathID " +
                                $"AND StateCourse = 1";
            var result = await connection.QueryAsync<Courses>(sqlQuery, new { PathID = id });
            connection.Close();
            return result.ToList();
        }

        public async Task<Courses> UpdateCourseAsync(UpdateCourse updateCourse)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var courseToUpdate = await GetCourseByIdAsync(updateCourse.CourseID);

            courseToUpdate.Title = updateCourse.Title;
            courseToUpdate.Description = updateCourse.Description;
            courseToUpdate.StateCourse = updateCourse.StateCourse;

            string sqlQuery = $"UPDATE {_tableNameCourses} SET Title = @Title, Description = @Description, StateCourse = @StateCourse WHERE CourseID = @CourseID";

            var result = await connection.ExecuteScalarAsync(sqlQuery, courseToUpdate);

            connection.Close();

            return _mapper.Map<Courses>(courseToUpdate);
        }

    }
}
