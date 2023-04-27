using CampusVirtual.Domain.Entities;
using CampusVirtual.Domain.Entities.Wrappers.Registration;

namespace CampusVirtual.UseCases.Gateway.Repositories
{
    public interface IRegistrationRepository
    {
        Task<string> CreateRegistrationAsync(Registration registration);
        Task<Registration> GetRegistrationByIDAsync(int registrationID);
        Task<List<RegistrationWithLearningPath>> GetAllRegistrationsAsync();
        Task<string> DeleteRegistrationAsync(int registrationID);
        //use cases
        Task<Registration> AverageFinalRatingAsync(string uidUser, string pathID);
    }
}