using Ardalis.GuardClauses;
using AutoMapper;
using CampusVirtual.Domain.Common;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;
using System.Text.Json;

namespace CampusVirtual.Infrastructure.SQLAdapter.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameRegistrations = "Registrations";
        private readonly IMapper _mapper;

        public RegistrationRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }
        #region Endpoints
        public async Task<Registration> AverageFinalRatingAsync(string uidUser, string pathID)
        {
            Guard.Against.NullOrEmpty(uidUser, nameof(uidUser));
            Guard.Against.NullOrEmpty(pathID, nameof(pathID));

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var registrationFound = await GetRegistrationByUidUserAndPathIDAsync(uidUser, pathID);
            Guard.Against.Null(registrationFound, nameof(registrationFound), $"There is no a registration available.");

            var ratings = await GetAssociatedRatingsAsync(registrationFound.UidUser, registrationFound.PathID.ToString());
            var finalRating = ratings.Average();

            registrationFound.SetFinalRating(finalRating);

            string query = $"UPDATE {_tableNameRegistrations} " +
                            $"SET finalRating = @FinalRating " +
                            $"WHERE registrationID = @RegistrationID";

            var result = await connection.ExecuteAsync(query, registrationFound);
            connection.Close();

            return result == 0 ? _mapper.Map<Registration>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : registrationFound;
        }

        public async Task<string> CreateRegistrationAsync(Registration registration)
        {
            Registration.SetDetailsRegistrationEntity(registration);
            Guard.Against.Null(registration, nameof(registration));
            Guard.Against.NullOrEmpty(registration.UidUser, nameof(registration.UidUser));
            Guard.Against.NullOrEmpty(registration.PathID, nameof(registration.PathID));
            Guard.Against.Null(registration.CreatedAt, nameof(registration.CreatedAt));
            Guard.Against.OutOfSQLDateRange(registration.CreatedAt, nameof(registration.CreatedAt));
            Guard.Against.Null(registration.FinalRating, nameof(registration.FinalRating));
            Guard.Against.OutOfRange(registration.FinalRating, nameof(registration.FinalRating), 0, 100);
            Guard.Against.Null(registration.StateRegistration, nameof(registration.StateRegistration));
            Guard.Against.EnumOutOfRange(registration.StateRegistration, nameof(registration.StateRegistration));


            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var registrationFound = await ValidateIfRegistrationExist(registration.UidUser, registration.PathID.ToString());
            if (registrationFound is null)
            {
                await connection.ExecuteAsync($"INSERT INTO {_tableNameRegistrations} " +
                                                $"(uidUser, pathID, createdAt, finalRating, stateRegistration) " +
                                                $"VALUES (@UidUser, @PathID, @CreatedAt, @FinalRating, @StateRegistration)",
                                                registration);
                connection.Close();
                return JsonSerializer.Serialize("The registration was created successfully.");
            }
            connection.Close();
            return JsonSerializer.Serialize("The registration was no created.");
        }

        public async Task<string> DeleteRegistrationAsync(int registrationID)
        {
            Guard.Against.Null(registrationID, nameof(registrationID));
            Guard.Against.NegativeOrZero(registrationID, nameof(registrationID));

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var registrationFound = await GetRegistrationByIDAsync(registrationID);

            if (registrationFound is not null)
            {
                await connection.ExecuteAsync($"UPDATE {_tableNameRegistrations} " +
                                                $"SET StateRegistration = @StateRegistration " +
                                                $"WHERE RegistrationID = @RegistrationID",
                                                new
                                                {
                                                    StateRegistration = Enums.StateRegistration.Deleted,
                                                    RegistrationID = registrationID
                                                });
                connection.Close();
                return JsonSerializer.Serialize("The registration was deleted successfully.");
            }
            connection.Close();
            return JsonSerializer.Serialize("The registration was not found.");
        }

        public async Task<List<Registration>> GetAllRegistrationsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var registrationsFound = (from reg in await connection.QueryAsync<Registration>
                                     ($"SELECT * FROM {_tableNameRegistrations}")
                                      where reg.StateRegistration == Enums.StateRegistration.Active
                                      select reg)
                                    .ToList();
            connection.Close();
            return registrationsFound.Count == 0
                ? _mapper.Map<List<Registration>>(Guard.Against.NullOrEmpty(registrationsFound, nameof(registrationsFound),
                    $"There is no registrations available."))
                : registrationsFound;
        }

        public async Task<Registration> GetRegistrationByIDAsync(int registrationID)
        {
            Guard.Against.Null(registrationID, nameof(registrationID));
            Guard.Against.NegativeOrZero(registrationID, nameof(registrationID));

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var registrationFound = (from reg in await connection.QueryAsync<Registration>
                                    ($"SELECT * FROM {_tableNameRegistrations}")
                                     where reg.RegistrationID == registrationID
                                     && reg.StateRegistration == Enums.StateRegistration.Active
                                     select reg)
                                    .FirstOrDefault();
            connection.Close();
            Guard.Against.Null(registrationFound, nameof(registrationFound), $"There is no a registration available.");
            return registrationFound;
        }
        #endregion

        #region Util methods
        private async Task<Registration?> ValidateIfRegistrationExist(string uidUser, string pathID)
        {
            var registrationFound = await GetRegistrationByUidUserAndPathIDAsync(uidUser, pathID);
            if (registrationFound != null)
            {
                throw new Exception("Your registration already exists. 409");
            }
            return registrationFound ?? null;
        }

        public async Task<List<decimal>> GetAssociatedRatingsAsync(string uidUser, string pathID)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var registrationFound = await GetRegistrationByUidUserAndPathIDAsync(uidUser, pathID);
            Guard.Against.Null(registrationFound, nameof(registrationFound), $"There is no a registration available.");

            var query = $"SELECT DISTINCT d.rating " +
                        $"FROM Registrations re " +
                        $"INNER JOIN LearningPaths lp ON re.pathID = lp.pathID " +
                        $"INNER JOIN Courses c ON lp.pathID = c.pathID " +
                        $"INNER JOIN Contents ct ON ct.courseID = c.courseID " +
                        $"INNER JOIN Deliveries d ON ct.contentID = d.contentID " +
                        $"WHERE d.uidUser = '{uidUser}' " +
                        $"AND d.rating IS NOT NULL";

            var ratingsFound = (from rating in await connection.QueryAsync<decimal>(query) select rating).ToList();
            connection.Close();
            return ratingsFound;
        }

        public async Task<Registration> GetRegistrationByUidUserAndPathIDAsync(string uidUser, string pathID)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var registrationFound = (from reg in await connection.QueryAsync<Registration>
                                     ($"SELECT * FROM {_tableNameRegistrations}")
                                     where reg.UidUser == uidUser && reg.PathID == Guid.Parse(pathID)
                                     && reg.StateRegistration == Enums.StateRegistration.Active
                                     select reg)
                                    .FirstOrDefault();
            connection.Close();
            return registrationFound;
        }
        #endregion
    }
}