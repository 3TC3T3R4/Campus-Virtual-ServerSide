using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.Courses
{
    public class UpdateDuration
    {
        public Guid CourseID { get; set; }
        public Decimal Duration { get; set; }
    }
}
