using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CampusVirtual.Domain.Entities
{
    public class Delivery
    {
        public int deliveryID { get; set; }
        public Guid contentID { get; set; }
        public string uidUser { get; set; }
        public DateTime deliveryAt { get; set; }
        public decimal rating { get; set; }
        public string comment { get; set; }
        public DateTime ratedAt { get; set; }
        public int stateDelivery { get; set; }
    }

}
