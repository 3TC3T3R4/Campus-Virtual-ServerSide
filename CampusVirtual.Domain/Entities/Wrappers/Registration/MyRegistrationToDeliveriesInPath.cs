using CampusVirtual.Domain.Common;

namespace CampusVirtual.Domain.Entities.Wrappers.Registration
{
    public class MyRegistrationToDeliveriesInPath
    {
        public int RegistrationID { get; set; }
        public string UidUser { get; set; }
        public Guid PathID { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal FinalRating { get; set; }
        public Enums.StateRegistration StateRegistration { get; set; }

        public List<decimal> MyRatings { get; set; } = new();
    }
}