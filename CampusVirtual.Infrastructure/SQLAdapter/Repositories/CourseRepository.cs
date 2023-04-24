using Ardalis.GuardClauses;
using AutoMapper;
using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var courseToCreate = new Courses
            {
                PathID = courses.PathID,
                Title = courses.Title,
                Description = courses.Description,
                Duration = courses.Duration,
                StateCourse = courses.StateCourse
            };

            Courses.Validate(courseToCreate);

            string sqlQuery = $"INSERT INTO {_tableNameCourses} (PathID, Title, Description, Duration, StateCourse)" +
                $"VALUES (@PathID, @Title, @Description, @Duration, @StateCourse)";

            var result = await connection.ExecuteScalarAsync(sqlQuery, courseToCreate);
            connection.Close();
            return _mapper.Map<NewCourse>(courseToCreate);
        }       

        public async Task<Courses> GetCourseByIdAsync(Guid id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCourses} WHERE CourseID = @CourseID";
            var result = await connection.QueryFirstOrDefaultAsync<Courses>(sqlQuery, new { CourseID = id });
            connection.Close();
            return result;
        }

        public async Task<Courses> DeleteCourseAsync(string id)
        {

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            string sqlQuery = $"UPDATE {_tableNameCourses} SET StateCourse = 0 WHERE CourseID = @CourseID";

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

        public async Task<Courses> GetCoursesByPathIdAsync(Guid id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCourses} WHERE PathID = @PathID";
            var result = await connection.QueryFirstOrDefaultAsync<Courses>(sqlQuery, new { PathID = id });
            connection.Close();
            return result;
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

        public async Task<Courses> AssingToPathAsync(AssingToPath assingToPath)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var courseToAssing = await GetCourseByIdAsync(assingToPath.CourseID);

            courseToAssing.PathID = assingToPath.PathID;

            if (courseToAssing.PathID == null)
            {
                throw new Exception("PathID cannot be null.");
            }

            string sqlQuery = $"UPDATE {_tableNameCourses} SET PathID = @PathID WHERE CourseID = @CourseID";

            var result = await connection.ExecuteScalarAsync(sqlQuery, courseToAssing);

            connection.Close();

            return _mapper.Map<Courses>(courseToAssing);
        }
       
    }
}
