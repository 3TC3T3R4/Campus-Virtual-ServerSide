using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.LearningPath
{
    public class InsertNewLearningPath
    {

        public string CoachID { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Duration { get; private set; }
        public int StatePath { get; private set; }





    }
}
