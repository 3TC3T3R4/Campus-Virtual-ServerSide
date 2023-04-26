﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.Courses
{
    public class UpdateCourse
    {
        public Guid CourseID { get; set; }       
        public string Title { get; set; }
        public string Description { get; set; }        
        public int StateCourse { get; set; }

    }
}