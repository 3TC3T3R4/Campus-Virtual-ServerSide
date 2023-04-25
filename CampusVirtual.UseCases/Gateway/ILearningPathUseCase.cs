using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;

namespace CampusVirtual.UseCases.Gateway
{
    public interface ILearningPathUseCase
    {
        Task<List<LearningPath>> GetLearningPathsAsync();
        Task<LearningPath> CreateLearningPathAsync(LearningPath learningPath);

        Task<List<LearningPath>> GetLearningPathsByCoachAsync(string coachID);

        Task<LearningPath> UpdateLearningPathByIdAsync(string idPath, UpdateLearningPaths path);
        Task<string> DeleteLearningPathByIdAsync(string idPath);


        Task<LearningPath> GetLearningPathByIdAsync(string coachID);

        Task<string> UpdateLearningPathDurationAsync(string idPath, decimal totalDuration);
    }
}
