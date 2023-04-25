using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string uidUser { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role { get; set; }
        public int stateUser { get; set; }
    }
}
