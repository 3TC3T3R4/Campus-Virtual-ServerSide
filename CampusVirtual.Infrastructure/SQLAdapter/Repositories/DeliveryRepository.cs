using Ardalis.GuardClauses;
using CampusVirtual.Domain.Commands.Delivery;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;

namespace CampusVirtual.Infrastructure.SQLAdapter.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly IDbConnectionBuilder _connectionBuilder;
        private readonly string tableName = "Deliveries";

        public DeliveryRepository(IDbConnectionBuilder connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }


        public async Task<string> CreateDelivery(CreateDelivery createDelivery)
        {
            Guard.Against.Null(createDelivery, nameof(createDelivery), "CreateDelivery is null");
            Guard.Against.NullOrEmpty(createDelivery.contentID, nameof(createDelivery.contentID), "Content ID is null or empty");
            Guard.Against.NullOrEmpty(createDelivery.uidUser, nameof(createDelivery.uidUser), "User ID is null or empty");
            Guard.Against.NullOrEmpty(createDelivery.DeliveryField, nameof(createDelivery.DeliveryField), "Delivery field is null or empty");


            var connection = await _connectionBuilder.CreateConnectionAsync();

            // Verificar si ya existe una entrega con el mismo contentID y uidUser
            string checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE contentID = @contentID AND uidUser = @uidUser AND stateDelivery = 1";
            int count = await connection.QuerySingleAsync<int>(checkQuery, new { contentID = createDelivery.contentID, uidUser = createDelivery.uidUser });

            // Si existe una entrega con el mismo contentID y uidUser, lanzar una excepción
            if (count > 0)
            {
                throw new ArgumentException("There is already a delivery with the same contentID and uidUser");
            }

            var newDelivery = new
            {
                contentID = createDelivery.contentID,
                uidUser = createDelivery.uidUser,
                deliveryAt = DateTime.Now,
                deliveryField = createDelivery.DeliveryField,
                stateDelivery = 1,
            };

            string sqlQuery = $"INSERT INTO {tableName} (contentID, uidUser, deliveryAt, deliveryField, stateDelivery) " +
                $"VALUES (@contentID, @uidUser, @deliveryAt, @deliveryField, @stateDelivery)";
            await connection.ExecuteAsync(sqlQuery, newDelivery);

            connection.Close();
            return "Delivery created";
        }


        public async Task<string> DeleteDelivery(int deliveryId)
        {
            Guard.Against.OutOfRange(deliveryId, nameof(deliveryId), 1, int.MaxValue, "Delivery ID is invalid");
            var connection = await _connectionBuilder.CreateConnectionAsync();

            // Check if delivery is not already deleted
            string checkQuery = $"SELECT stateDelivery FROM {tableName} WHERE deliveryID = {deliveryId}";
            int currentState = await connection.ExecuteScalarAsync<int>(checkQuery);
            if (currentState == 2)
            {
                connection.Close();
                return "Delivery already deleted";
            }

            // Delete delivery
            var deleteDelivery = new { stateDelivery = 2 };
            string updateQuery = $"UPDATE {tableName} SET stateDelivery = @stateDelivery WHERE deliveryID = {deliveryId} AND stateDelivery = 1";
            await connection.ExecuteAsync(updateQuery, deleteDelivery);
            connection.Close();
            return "Delivery deleted";
        }


        public async Task<Delivery> GetDeliveryById(int deliveryId)
        {
            Guard.Against.OutOfRange(deliveryId, nameof(deliveryId), 1, int.MaxValue, "Delivery ID is invalid");
            var connection = await _connectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {tableName} WHERE deliveryID = @deliveryId AND stateDelivery = 1";
            var delivery = await connection.QueryFirstOrDefaultAsync<Delivery>(sqlQuery, new
            {
                deliveryId
            });
            Guard.Against.Null(delivery, nameof(delivery), "There is no a delivery available.");
            connection.Close();
            return delivery;
        }


        public async Task<List<Delivery>> GetDeliveriesByUidUser(string uidUser)
        {
            Guard.Against.NullOrEmpty(uidUser, nameof(uidUser), "User ID is null or empty");
            var connection = await _connectionBuilder.CreateConnectionAsync();
            var sqlQuery = "SELECT * FROM Deliveries WHERE uidUser = @UidUser AND stateDelivery = 1";
            var parameters = new { UidUser = uidUser };
            var command = new CommandDefinition(sqlQuery, parameters);
            var deliveries = await connection.QueryAsync<Delivery>(command);
            Guard.Against.NullOrEmpty(deliveries, nameof(deliveries), "There are no deliveries available.");
            connection.Close();
            return deliveries.ToList();
        }



        public async Task<string> QualifyDelivery(QualifyDelivery qualifyDelivery)
        {
            Guard.Against.Null(qualifyDelivery, nameof(qualifyDelivery), "QualifyDelivery is null");
            Guard.Against.OutOfRange(qualifyDelivery.deliveryID, nameof(qualifyDelivery.deliveryID), 1, int.MaxValue, "Delivery ID is invalid");
            Guard.Against.OutOfRange(qualifyDelivery.rating, nameof(qualifyDelivery.rating), 0, 100);
            Guard.Against.NullOrEmpty(qualifyDelivery.comment, nameof(qualifyDelivery.comment), "Comment is null or empty");


            var connection = await _connectionBuilder.CreateConnectionAsync();
            var delivery = await GetDeliveryById(qualifyDelivery.deliveryID);
            if (delivery == null)
            {
                throw new ArgumentException($"Delivery with ID {qualifyDelivery.deliveryID} does not exist");
            }
            if (delivery.stateDelivery == 2)
            {
                throw new InvalidOperationException($"Delivery with ID {qualifyDelivery.deliveryID} has already been deleted or qualified");
            }

            var newDelivery = new
            {
                rating = qualifyDelivery.rating,
                comment = qualifyDelivery.comment,
                ratedAt = DateTime.Now,
            };
            string sqlQuery =
                $"UPDATE {tableName} SET rating = @rating, comment = @comment, ratedAt = @ratedAt WHERE deliveryID = {qualifyDelivery.deliveryID}";
            await connection.ExecuteAsync(sqlQuery, newDelivery);
            connection.Close();
            return "Delivery qualified";
        }

        public async Task<List<Delivery>> GetDeliveriesByPathId(string pathID)
        {
            Guard.Against.NullOrEmpty(pathID, nameof(pathID), "Path ID is null or empty");
            var connection = await _connectionBuilder.CreateConnectionAsync();
            var sqlQuery = @"SELECT d.*
                        FROM Deliveries d
                        INNER JOIN Contents ct ON d.contentID = ct.contentID
                        INNER JOIN Courses c ON ct.courseID = c.courseID 
                        INNER JOIN LearningPaths lp ON c.pathID = lp.pathID 
                        WHERE lp.pathID =  @PathId
                        AND d.rating IS NULL
                        AND d.comment IS NULL
                        AND d.ratedAt IS NULL
                        AND d.stateDelivery = 1";
            var parameters = new { PathId = pathID };
            var command = new CommandDefinition(sqlQuery, parameters);
            var deliveries = await connection.QueryAsync<Delivery>(command);
            Guard.Against.NullOrEmpty(deliveries, nameof(deliveries), "There are no deliveries available.");
            connection.Close();
            return deliveries.ToList();
        }
    }
}