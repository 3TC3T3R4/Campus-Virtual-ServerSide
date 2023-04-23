using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.UseCases.Gateway.Repositories
{
    public interfaces ILearningPath
    {

        Task<List<LearningPath>> GetLearningPathsAsync();



    }
}
