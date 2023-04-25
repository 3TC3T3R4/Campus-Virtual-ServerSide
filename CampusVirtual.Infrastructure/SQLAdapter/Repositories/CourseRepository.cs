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
              

        public async Task<NewCourse> CreateCourseAsync(Courses courses)///
        {
            Guard.Against.NullOrEmpty(courses.Description, nameof(courses.Description));
            Guard.Against.NullOrEmpty(courses.Title, nameof(courses.Title));
            Guard.Against.NullOrEmpty(courses.Duration.ToString(), nameof(courses.Duration));
            Guard.Against.NullOrEmpty(courses.StateCourse.ToString(), nameof(courses.StateCourse));            
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();          

            var courseToCreate = new Courses
            {
                Title = courses.Title,
                Description = courses.Description,
                Duration = courses.Duration,
                StateCourse = courses.StateCourse
            };

            Courses.Validate(courseToCreate);

            string sqlQuery = $"INSERT INTO {_tableNameCourses} (Title, Description, Duration, StateCourse)" +
                $"VALUES (@Title, @Description, @Duration, @StateCourse)";

            var result = await connection.ExecuteScalarAsync(sqlQuery, courseToCreate);
            connection.Close();
            return _mapper.Map<NewCourse>(courseToCreate);
        }

        public async Task<Courses> GetCourseByIdAsync(Guid id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            if (connection == null)
            {
                throw new Exception("No se pudo establecer la conexión a la base de datos.");
            }

            string sqlQuery = $"SELECT * FROM {_tableNameCourses} WHERE CourseID = @CourseID";
            var result = await connection.QueryAsync<Courses>(sqlQuery, new { CourseID = id });

            connection.Close();

            return result.FirstOrDefault();
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

            return _mapper.Map<Courses>(result);
           
        }

        public async Task<Courses> UpdateDurationAsync(UpdateDuration updateDuration)////
        {

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var durationToUpdate = await GetCourseByIdAsync(updateDuration.CourseID);

            if (updateDuration.Duration == null)
            {
                throw new Exception("Duration cannot be null.");
            }

            durationToUpdate.Duration += updateDuration.Duration;

            string sqlQuery = $"UPDATE {_tableNameCourses} SET Duration = @Duration WHERE CourseID = @CourseID";

            var result = await connection.ExecuteScalarAsync(sqlQuery, durationToUpdate);

            connection.Close();

            return _mapper.Map<Courses>(durationToUpdate);

        }

        public async Task<List<Courses>> GetCoursesByPathIdAsync(Guid id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCourses} WHERE PathID = @PathID AND StateCourse = 2";
            var result = await connection.QueryAsync<Courses>(sqlQuery, new { PathID = id });

            if(result.Count() == 0)
            {
                throw new Exception("Path cannot be null or empty.");
            }
                                
            connection.Close();
            return result.ToList();
            
        }
            

        public async Task<Courses> UpdateCourseAsync(UpdateCourse updateCourse)//
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var courseToUpdate = await GetCourseByIdAsync(updateCourse.CourseID);

            if (string.IsNullOrEmpty(updateCourse.Title))
            {
                throw new Exception("Title cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(updateCourse.Description))
            {
                throw new Exception("Description cannot be null or empty.");
            }

            if (updateCourse.StateCourse == null)
            {
                throw new Exception("StateCourse cannot be null.");
            }

            courseToUpdate.Title = updateCourse.Title;
            courseToUpdate.Description = updateCourse.Description;
            courseToUpdate.StateCourse = updateCourse.StateCourse;

            string sqlQuery = $"UPDATE {_tableNameCourses} SET Title = @Title, Description = @Description, StateCourse = @StateCourse WHERE CourseID = @CourseID";

            var result = await connection.ExecuteScalarAsync(sqlQuery, courseToUpdate);

            connection.Close();

            return _mapper.Map<Courses>(courseToUpdate);
        }

        public async Task<Courses> ConfigureToPathAsync(AssingToPath assingToPath)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var courseToAssing = await GetCourseByIdAsync(assingToPath.CourseID);

            Guard.Against.Null(courseToAssing, nameof(courseToAssing));            

            if (courseToAssing.PathID == null)
            {
                courseToAssing.PathID = assingToPath.PathID;
            }
            else
            {
                if(courseToAssing.PathID == assingToPath.PathID)
                {
                    courseToAssing.PathID = Guid.Empty;
                }
            }


            string sqlQuery = $"UPDATE {_tableNameCourses} SET PathID = @PathID WHERE CourseID = @CourseID";

            var result = await connection.ExecuteScalarAsync(sqlQuery, courseToAssing);

            connection.Close();

            return _mapper.Map<Courses>(courseToAssing);
        }
       
    }
}
