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

        int deliveryID { get; set; }
        Guid contentID { get; set; }
        string uidUser { get; set; }
        DateTime deliveryAt { get; set; }
        decimal rating { get; set; }
        string comment { get; set; }
        DateTime ratedAt { get; set; }
        int stateDelivery { get; set; }

    }
}
