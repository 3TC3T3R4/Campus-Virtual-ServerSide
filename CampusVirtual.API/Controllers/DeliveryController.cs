using AutoMapper;
using CampusVirtual.Domain.Commands.Delivery;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace CampusVirtual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryUseCase _deliveryUseCase;
        private readonly IMapper _mapper;

        public DeliveryController(IDeliveryUseCase deliveryUseCase, IMapper mapper)
        {
            _deliveryUseCase = deliveryUseCase;
            _mapper = mapper;
        }

        //CreateDelivery
        [HttpPost]
        public async Task<string> CreateDelivery(CreateDelivery createDelivery)
        {
            return await _deliveryUseCase.CreateDelivery(createDelivery);
        }

        //DeleteDelivery
        [HttpDelete]
        public async Task<string> DeleteDelivery(int deliveryID)
        {
            return await _deliveryUseCase.DeleteDelivery(deliveryID);
        }

        //GetDeliveryById
        [HttpGet("ById/")]
        public async Task<Delivery> GetDeliveryById(int deliveryID)
        {
            return await _deliveryUseCase.GetDeliveryById(deliveryID);
        }

        //GetDeliveriesByUidUser
        [HttpGet("ByUidUsers/")]

        public async Task<List<Delivery>> GetDeliveriesByUidUser(string uidUser)
        {
            return await _deliveryUseCase.GetDeliveriesByUidUser(uidUser);
        }

        //QualifyDelivery
        [HttpPatch]
        public async Task<string> QualifyDelivery(QualifyDelivery qualifyDelivery)
        {
            return await _deliveryUseCase.QualifyDelivery(qualifyDelivery);
        }

        //GetDeliveriesByPathId
        [HttpGet("ByPathId/")]
        public async Task<List<Delivery>> GetDeliveriesByPathId(string pathID)
        {
            return await _deliveryUseCase.GetDeliveriesByPathId(pathID);
        }
    }
}
