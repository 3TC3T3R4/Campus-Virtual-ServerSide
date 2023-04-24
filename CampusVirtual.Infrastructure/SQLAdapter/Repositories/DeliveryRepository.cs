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
            return "Delivery created";
        }

        public async Task<string> DeleteDelivery(int deliveryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Delivery> GetDeliveryById(int deliveryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Delivery> GetDeliveriesByUidUser(string uidUser)
        {
            throw new NotImplementedException();
        }

        public async Task<string> QualifyDelivery(QualifyDelivery qualifyDelivery)
        {
            throw new NotImplementedException();
        }
    }
}
