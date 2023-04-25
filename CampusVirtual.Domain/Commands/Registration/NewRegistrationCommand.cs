namespace CampusVirtual.Domain.Commands.Registration
{
    public class NewRegistrationCommand
    {
        public string UidUser { get; set; }
        public Guid PathID { get; set; }
    }
}