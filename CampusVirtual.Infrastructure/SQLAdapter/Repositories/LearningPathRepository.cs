using Ardalis.GuardClauses;
using AutoMapper;
using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace CampusVirtual.Infrastructure.SQLAdapter.Repositories
{
    public class LearningPathRepository: ILearningPathRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly IMapper _mapper;

        private readonly string _tableNameLearningPaths = "LearningPaths";
       // private readonly string _tableNameRegistrations = "Registrations";

        public LearningPathRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async  Task<List<LearningPath>> GetLearningPathsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameLearningPaths} WHERE statePath = 1";
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


        public async Task<LearningPath> CreateLearningPathAsync(LearningPath learningPath)
        {

            Guard.Against.Null(learningPath, nameof(learningPath));
            Guard.Against.NullOrEmpty(learningPath.CoachID, nameof(learningPath.CoachID), "Ingresa por favor el id del coach, no puede ser vacio o nulo");
            Guard.Against.NullOrEmpty(learningPath.Title, nameof(learningPath.Title), "Ingresa un  titulo por favor, no puedes dejar el campo como nulo o vacio");
            Guard.Against.NullOrEmpty(learningPath.Description, nameof(learningPath.Description), "No puedes ingresar una descripcion vacia o nula, por favor ingresa alguna descripcion");
            //Guard.Against.NullOrEmpty(learningPath.Duration.ToString(),nameof(learningPath.Duration), "Ingresa por favor una duracion, no puede ser nula o vacia");
          //  Guard.Against.NullOrEmpty(learningPath.StatePath.ToString() ,nameof(learningPath.StatePath));

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var createLearnigP = new 
            {
                CoachID = learningPath.CoachID,
                Title = learningPath.Title,
                Description = learningPath.Description,
                Duration = 0,
                StatePathB  = 1
            };
           // LearningPath.Validate(createLearnigP);

            string sqlQuery = $"INSERT INTO {_tableNameLearningPaths} (CoachID,Title,Description,Duration,StatePath) VALUES (@CoachID,@Title,@Description,@Duration,@StatePathB)";
            var rows = await connection.ExecuteAsync(sqlQuery, createLearnigP);
            return learningPath;
        }


        public async Task<List<LearningPath>> GetLearningPathsByCoachAsync(string ID)
        {

           Guard.Against.Null(ID, nameof(ID), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(ID, nameof(ID), "Ingresa por favor el id del coach, no puede ser vacio o nulo");


            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameLearningPaths}  WHERE  coachID " +
                $" =  '{ID}' AND statePath = 1"; 
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

        public async Task<LearningPath> UpdateLearningPathByIdAsync(string idPath, UpdateLearningPaths path)
        {
            Guard.Against.Null(idPath, nameof(idPath), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(idPath, nameof(idPath), "Ingresa por favor el id  no puede ser vacio o nulo");

            Guard.Against.Null(path.CoachID, nameof(path.CoachID), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(path.CoachID, nameof(path.CoachID), "Ingresa por favor el id  no puede ser vacio o nulo");

            Guard.Against.Null(path.Title, nameof(path.Title), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(path.Title, nameof(path.Title), "Ingresa por favor el id  no puede ser vacio o nulo");

            Guard.Against.Null(path.Description , nameof(path.Description), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(path.Description, nameof(path.Description), "Ingresa por favor el id  no puede ser vacio o nulo");


            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"UPDATE {_tableNameLearningPaths} " +
                $"SET coachID = @coachID, title = @title, description = @description  WHERE pathID = '{idPath}'";
            var rows = await connection.ExecuteAsync(sqlQuery, path);
            return _mapper.Map<LearningPath>(path);
        }

        public async Task<string> DeleteLearningPathByIdAsync(string idPath)
        {
            Guard.Against.Null(idPath, nameof(idPath), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(idPath, nameof(idPath), "Ingresa por favor el id del coach, no puede ser vacio o nulo");


            var param = new { delete = 2 };
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"UPDATE {_tableNameLearningPaths} SET statePath = @delete WHERE  pathID = '{idPath}'";
            var result = await connection.ExecuteAsync(sqlQuery, param);
            connection.Close();
            return JsonSerializer.Serialize("Path deleted");
        }

        public async Task<LearningPath> GetLearningPathsByIdAsync(string idPath)
        {
            Guard.Against.Null(idPath, nameof(idPath), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(idPath, nameof(idPath), "Ingresa por favor el id del coach, no puede ser vacio o nulo");

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameLearningPaths}  WHERE  pathID " +
                $" =  '{idPath}' AND statePath = 1";
            var result = await connection.QueryFirstOrDefaultAsync<LearningPath>(sqlQuery);
            if(result == null)
            {
                throw new Exception("No LearningPaths found");
            }
            connection.Close();
            return result;
        }

        public async Task<string> UpdateLearningPathDurationAsync(string idPath, decimal totalDuration)
        {
            Guard.Against.Null(idPath, nameof(idPath), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(idPath, nameof(idPath), "Ingresa por favor el id del coach, no puede ser vacio o nulo");
            Guard.Against.Null(totalDuration, nameof(totalDuration), "Ingresa el campo por favor");
            Guard.Against.NullOrEmpty(totalDuration.ToString(), nameof(totalDuration), "Ingresa por favor el id del coach, no puede ser vacio o nulo");

            var numberConverted = decimal.Parse(totalDuration.ToString().Replace(",", "."));
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"UPDATE {_tableNameLearningPaths} SET duration = {numberConverted} WHERE pathID = '{idPath}' ";
            var result = await connection.ExecuteAsync(sqlQuery);
            connection.Close();
            return JsonSerializer.Serialize("Duration Updated");
        }
    }
}
