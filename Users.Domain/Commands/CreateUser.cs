using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.Commands
{
    public class CreateUser
    {
        public string uidUser { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role { get; set; }
    }
}
