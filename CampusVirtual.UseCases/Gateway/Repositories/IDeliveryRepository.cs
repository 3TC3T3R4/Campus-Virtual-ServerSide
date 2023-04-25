﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusVirtual.Domain.Commands.Delivery;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.UseCases.Gateway.Repositories
{
    public interface IDeliveryRepository
    {
        Task<string> CreateDelivery(CreateDelivery createDelivery);
        Task<string> DeleteDelivery(int deliveryId);
        Task<Delivery> GetDeliveryById(int deliveryId);
        Task<List<Delivery>> GetDeliveriesByUidUser(string uidUser);
        Task<string> QualifyDelivery(QualifyDelivery qualifyDelivery);
        Task<List<Delivery>> GetDeliveriesByPathId(string pathId);
    }
}
