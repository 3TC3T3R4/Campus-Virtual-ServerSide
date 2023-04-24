using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var connection = await _connectionBuilder.CreateConnectionAsync();
            var newDelivery = new
            {
                contentID = createDelivery.contentID,
                uidUser = createDelivery.uidUser,
                deliveryAt = DateTime.Now,
                rating = 0,
                comment = "",
                ratedAt = DateTime.Now,
                stateDelivery = 1,

            };
            string sqlQuery =
                $"INSERT INTO {tableName} (contentID, uidUser, deliveryAt, rating, comment, ratedAt, stateDelivery) VALUES (@contentID, @uidUser, @deliveryAt, @rating, @comment, @ratedAt, @stateDelivery)";
            await connection.ExecuteAsync(sqlQuery, newDelivery);
            connection.Close();
            return "Delivery created";
        }

        public async Task<string> DeleteDelivery(int deliveryId)
        {
            var connection = await _connectionBuilder.CreateConnectionAsync();
            var deteleDelivery = new
            {
                stateDelivery = 2,
            };
            string sqlQuery = $"UPDATE {tableName} SET stateDelivery = @stateDelivery WHERE deliveryID = {deliveryId}";
            await connection.ExecuteAsync(sqlQuery, deteleDelivery);
            connection.Close();
            return "Delivery deleted";
        }

        public async Task<Delivery> GetDeliveryById(int deliveryId)
        {
            var connection = await _connectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {tableName} WHERE deliveryID = {deliveryId}";
            var delivery = await connection.QueryFirstOrDefaultAsync<Delivery>(sqlQuery);
            connection.Close();
            return delivery;
        }

        public async Task<List<Delivery>> GetDeliveriesByUidUser(string uidUser)
        {
            var connection = await _connectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {tableName} WHERE uidUser = {uidUser}";
            var delivery = await connection.QueryFirstOrDefaultAsync<List<Delivery>>(sqlQuery);
            connection.Close();
            return delivery.ToList();
        }

        public async Task<string> QualifyDelivery(QualifyDelivery qualifyDelivery)
        {
            var connection = await _connectionBuilder.CreateConnectionAsync();
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
    }
}
