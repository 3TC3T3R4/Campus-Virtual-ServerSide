using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.Courses
{
    public class AssingToPath
    {
        public Guid CourseID { get; set; }
        public Guid PathID { get; set; }         

    }
}
