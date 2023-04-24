using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.Delivery
{
    public class QualifyDelivery
    {
        int deliveryID { get; set; }
        decimal rating { get; set; }
        string comment { get; set; }
    }
}
