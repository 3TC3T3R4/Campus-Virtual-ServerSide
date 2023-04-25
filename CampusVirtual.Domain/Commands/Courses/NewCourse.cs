using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.Courses
{
    public class NewCourse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Duration { get; set; }
        public int StateCourse { get; set; }
    }
}
