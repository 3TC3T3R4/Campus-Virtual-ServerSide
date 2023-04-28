using CampusVirtual.Domain.Common;

namespace CampusVirtual.Domain.Entities.Wrappers.Registration
{
    public class RegistrationWithLearningPath
    {
        public int RegistrationID { get; set; }
        public string UidUser { get; set; }
        public Guid PathID { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal FinalRating { get; set; }
        public Enums.StateRegistration StateRegistration { get; set; }
        public string CoachID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Duration { get; set; }
        public int StatePath { get; set; }
    }
}
