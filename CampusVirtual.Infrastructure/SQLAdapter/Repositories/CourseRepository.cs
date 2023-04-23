using AutoMapper;
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

    }
}
