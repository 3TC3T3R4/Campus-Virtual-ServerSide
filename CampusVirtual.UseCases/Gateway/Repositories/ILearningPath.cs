using CampusVirtual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.UseCases.Gateway.Repositories
{
    public interface ILearningPath
    {

        Task<List<LearningPath>> GetLearningPathsAsync();



    }
}
