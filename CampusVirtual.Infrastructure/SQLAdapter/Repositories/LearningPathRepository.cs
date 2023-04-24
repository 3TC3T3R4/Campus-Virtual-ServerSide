using AutoMapper;
using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Infrastructure.SQLAdapter.Repositories
{
    public class LearningPathRepository: ILearningPathRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly IMapper _mapper;

        private readonly string _tableNameLearningPaths = "LearningPaths";
        private readonly string _tableNameRegistrations = "Registrations";

        public LearningPathRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async  Task<List<LearningPath>> GetLearningPathsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameLearningPaths}";
            var result = await connection.QueryAsync<LearningPath>(sqlQuery);
            if
            (
                result.IsNullOrEmpty()
            )
            {
                throw new Exception("No LearningPaths found");
            }
            connection.Close();
            return result.ToList();
        }


        public async Task<InsertNewLearningPath> CreateLearningPathAsync(LearningPath learningPath)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var createLearnigP = new LearningPath
            {
                CoachID = learningPath.CoachID,
                Title = learningPath.Title,
                Description = learningPath.Description,
                Duration = learningPath.Duration,
                StatePath  = learningPath.StatePath
        

            };
           // LearningPath.Validate(createLearnigP);

            string sqlQuery = $"INSERT INTO {_tableNameLearningPaths} (CoachID,Title,Description,Duration,StatePath) VALUES (@CoachID,@Title,@Description,@Duration,@StatePath )";
            var result = await connection.ExecuteAsync(sqlQuery, createLearnigP);
            connection.Close();
            return _mapper.Map<InsertNewLearningPath>(createLearnigP);

        }


        public async Task<List<LearningPath>> GetLearningPathsByCoachAsync(string ID)
        {

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameLearningPaths}  WHERE  coachID " +
                $" =  '{ID}'"; 
            var result = await connection.QueryAsync<LearningPath>(sqlQuery);
            if
            (
                result.IsNullOrEmpty()
            )
            {
                throw new Exception("No LearningPaths found");
            }
            connection.Close();
            return result.ToList();




        }
    }
}
