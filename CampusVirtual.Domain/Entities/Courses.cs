using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Entities
{
    public class Courses
    {
        public Guid CourseID { get; set; }
        public Guid PathID { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public Decimal Duration { get; set; }
        public int StateCouse { get; set; }
    }
}
