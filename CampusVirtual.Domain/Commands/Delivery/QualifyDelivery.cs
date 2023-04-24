using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.Delivery
{
    public class QualifyDelivery
    {
        public int deliveryID { get; set; }
        public decimal rating { get; set; }
        public string comment { get; set; }
    }
}
