using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusVirtual.Domain.Commands.Delivery;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;

namespace CampusVirtual.UseCases.UseCases
{
    public class DeliveryUseCase : IDeliveryUseCase
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryUseCase(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<string> CreateDelivery(CreateDelivery createDelivery)
        {
            return await _deliveryRepository.CreateDelivery(createDelivery);
        }

        public async Task<string> DeleteDelivery(int deliveryId)
        {
            return await _deliveryRepository.DeleteDelivery(deliveryId);
        }

        public async Task<Delivery> GetDeliveryById(int deliveryId)
        {
            return await _deliveryRepository.GetDeliveryById(deliveryId);
        }

        public async Task<List<Delivery>> GetDeliveriesByUidUser(string uidUser)
        {
            return await _deliveryRepository.GetDeliveriesByUidUser(uidUser);
        }

        public async Task<string> QualifyDelivery(QualifyDelivery qualifyDelivery)
        {
            return await _deliveryRepository.QualifyDelivery(qualifyDelivery);
        }

        public async Task<List<Delivery>> GetDeliveriesByPathId(string pathId)
        {
            return await _deliveryRepository.GetDeliveriesByPathId(pathId);
        }
    }
}
