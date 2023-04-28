using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.UseCases.Gateway.Repositories
{
    public interface ILearningPathRepository
    {
        Task<List<LearningPath>> GetLearningPathsAsync();
        Task<LearningPath> CreateLearningPathAsync(LearningPath learningPath);
        Task<List<LearningPath>> GetLearningPathsByCoachAsync(string coachID);
        Task<List<LearningPath>> GetLearningPathsByTraineeAsync(string traineeID);
        Task<LearningPath> UpdateLearningPathByIdAsync(string idPath, UpdateLearningPaths path);
        Task<string> DeleteLearningPathByIdAsync(string idPath);
        Task<LearningPath> GetLearningPathsByIdAsync(string idPath);
        Task<string> UpdateLearningPathDurationAsync(string idPath, decimal totalDuration);
    }
}
