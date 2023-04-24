using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.UseCases.Gateway
{
    public interface ILearningPathUseCase
    {
        Task<List<LearningPath>> GetLearningPathsAsync();
        Task<InsertNewLearningPath> CreateLearningPathAsync(LearningPath learningPath);

        Task<List<LearningPath>> GetLearningPathsByCoachAsync(string coachID);

        Task<InsertNewLearningPath> UpdateLearningPathByIdAsync(string idPath, InsertNewLearningPath path);
        Task<string> DeleteLearningPathByIdAsync(string idPath);


        Task<LearningPath>GetLearningPathByIdAsync(string coachID);

        Task<string> UpdateLearningPathDurationAsync(string idPath, int numberCourses);
    }
}
