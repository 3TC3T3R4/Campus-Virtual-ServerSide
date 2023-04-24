using CampusVirtual.Domain.Common;

namespace CampusVirtual.Domain.Entities
{
    public class Registration
    {
        public int RegistrationID { get; private set; }
        public string UidUser { get; private set; }
        public Guid PathID { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public decimal FinalRating { get; private set; }
        public Enums.StateRegistration StateRegistration { get; private set; }

        #region Constructors
        public Registration() { }
        #endregion

        #region Methods
        public static Registration SetDetailsRegistrationEntity(Registration registration)
        {
            registration.CreatedAt = DateTime.Now;
            registration.FinalRating = 0;
            registration.StateRegistration = Enums.StateRegistration.Active;
            return registration;
        }
        #endregion

        #region Setters
        public void SetRegistrationID(int id)
        {
            RegistrationID = id;
        }
        public void SetUidUser(string id)
        {
            UidUser = id;
        }
        public void SetPathID(Guid id)
        {
            PathID = id;
        }
        public void SetCreatedAt(DateTime date)
        {
            CreatedAt = date;
        }
        public void SetFinalRating(decimal rating)
        {
            FinalRating = rating;
        }
        public void SetRegistrationState(Enums.StateRegistration state)
        {
            StateRegistration = state;
        }
        #endregion
    }
}