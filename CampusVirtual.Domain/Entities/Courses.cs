using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Entities
{
    public class Courses
    {
        public Guid CourseID { get;  set; }
        public Guid PathID { get;  set; }
        public string Title { get;  set; }
        public string Description { get;  set; }
        public Decimal Duration { get;  set; }
        public int StateCourse { get;  set; }

        public Courses()
        {           
        }

        public static Courses SetDetailsCoursesEntity(Courses courses)
        {
            courses.PathID = courses.PathID;
            courses.Title = courses.Title;
            courses.Description = courses.Description;
            courses.Duration = courses.Duration;
            courses.StateCourse = courses.StateCourse;

            return courses;
        }

        public void SetCourseID(Guid courseID)
        {
            CourseID = courseID;
        }

        public void SetPathID(Guid pathID)
        {
            PathID = pathID;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetDuration(Decimal duration)
        {
            Duration = duration;
        }

        public void SetStateCourse(int stateCourse)
        {
            StateCourse = stateCourse;
        }
       


    }
}
