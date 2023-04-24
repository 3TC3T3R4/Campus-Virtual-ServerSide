using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.Delivery
{
    public class CreateDelivery
    {
        public Guid contentID { get; set; }
        public string uidUser { get; set; }
    }
}
