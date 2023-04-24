using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Entities
{
    public class LearningPath
    {
        public Guid PathID { get; set; }
        public string CoachID { get;  set; }
        public string Title { get;  set; }
        public string Description { get;   set; }
        public decimal Duration { get;   set; }
        public int StatePath { get;   set; }

        public LearningPath()
        {
        }

        







    }
}
