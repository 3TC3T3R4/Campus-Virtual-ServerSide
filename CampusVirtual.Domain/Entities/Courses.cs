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
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Duration { get; set; }
        public int StateCourse { get; set; }

        public Courses()
        {
        }

        public static void Validate(Courses courses)
        {
            if (courses.PathID == null)
            {
                throw new Exception("PathID is required");
            }
            if (courses.Title == null)
            {
                throw new Exception("Title is required");
            }
            if (courses.Description == null)
            {
                throw new Exception("Description is required");
            }
            if (courses.Duration == null)
            {
                throw new Exception("Duration is required");
            }
            if (courses.StateCourse == null)
            {
                throw new Exception("StateCourse is required");
            }

        }
    }
}
