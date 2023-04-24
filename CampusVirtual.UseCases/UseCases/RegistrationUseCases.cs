using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;

namespace CampusVirtual.UseCases.UseCases
{
    public class RegistrationUseCases : IRegistrationUseCases
    {
        private readonly IRegistrationRepository _registrationRepository;
        public RegistrationUseCases(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<Registration> AverageFinalRatingAsync(string uidUser, Guid pathID)
        {
            return await _registrationRepository.AverageFinalRatingAsync(uidUser, pathID);
        }

        public async Task<string> CreateRegistrationAsync(Registration registration)
        {
            return await _registrationRepository.CreateRegistrationAsync(registration);
        }

        public async Task<string> DeleteRegistrationAsync(int registrationID)
        {
            return await _registrationRepository.DeleteRegistrationAsync(registrationID);
        }

        public async Task<List<Registration>> GetAllRegistrationsAsync()
        {
            return await _registrationRepository.GetAllRegistrationsAsync();
        }

        public async Task<Registration> GetRegistrationByIDAsync(int registrationID)
        {
            return await _registrationRepository.GetRegistrationByIDAsync(registrationID);
        }
    }
}