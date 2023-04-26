namespace CampusVirtual.Domain.Commands.Delivery
{
    public class CreateDelivery
    {
        public Guid contentID { get; set; }
        public string uidUser { get; set; }
        public string DeliveryField { get; set; }
    }
}