using AutoMapper;
using CampusVirtual.Domain.Commands.Registration;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace CampusVirtual.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationUseCases _registrationUseCases;
        private readonly IMapper _mapper;

        public RegistrationController(IRegistrationUseCases registrationUseCases, IMapper mapper)
        {
            _registrationUseCases = registrationUseCases;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<string> CreateRegistrationAsync([FromBody] NewRegistrationCommand newRegistrationCommand)
        {
            return await _registrationUseCases.CreateRegistrationAsync(_mapper.Map<Registration>(newRegistrationCommand));
        }

        [HttpGet]
        public async Task<List<Registration>> GetAllRegistrationsAsync()
        {
            return await _registrationUseCases.GetAllRegistrationsAsync();
        }

        [HttpGet("ID")]
        public async Task<Registration> GetRegistrationByIDAsync(int registrationID)
        {
            return await _registrationUseCases.GetRegistrationByIDAsync(registrationID);
        }

        [HttpDelete("ID")]
        public async Task<string> DeleteRegistrationAsync(int registrationID)
        {
            return await _registrationUseCases.DeleteRegistrationAsync(registrationID);
        }

        [HttpPatch("AverageFinalRating")]
        public async Task<Registration> AverageFinalRatingAsync(string uidUser, Guid pathID)
        {
            return await _registrationUseCases.AverageFinalRatingAsync(uidUser, pathID);
        }
    }
}