using Ardalis.GuardClauses;
using AutoMapper;
using CampusVirtual.Domain.Commands.Courses;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
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

        public async Task<UpdateCourse> UpdateCourseAsync(Courses courses)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            Courses.SetDetailsCoursesEntity(courses);

            var courseToUpdate = new Courses
            {
                PathID = courses.PathID,
                Title = courses.Title,
                Description = courses.Description,
                Duration = courses.Duration,
                StateCourse = courses.StateCourse
            };

            string sqlQuery = $"UPDATE {_tableNameCourses} SET PathID = @PathID, Title = @Title, Description = @Description, Duration = @Duration, StateCourse = @StateCourse WHERE CourseID = @CourseID";

            var result = await connection.ExecuteScalarAsync(sqlQuery, courseToUpdate);
            connection.Close();
            return _mapper.Map<UpdateCourse>(courseToUpdate);
        }

        public async Task<Courses> GetCourseByIdAsync(Guid id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCourses} WHERE CourseID = @CourseID";
            var result = await connection.QueryFirstOrDefaultAsync<Courses>(sqlQuery, new { CourseID = id });
            connection.Close();
            return result;
        }
       
    }
}
