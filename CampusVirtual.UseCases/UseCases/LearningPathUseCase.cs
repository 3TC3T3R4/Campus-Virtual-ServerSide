using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;

namespace CampusVirtual.UseCases.UseCases
{
    public class LearningPathUseCase : ILearningPathUseCase
    {
        private readonly ILearningPathRepository _learningPathRepository;

        public LearningPathUseCase(ILearningPathRepository learningPathRepository)
        {
            _learningPathRepository = learningPathRepository;
        }

        public async Task<LearningPath> CreateLearningPathAsync(LearningPath learningPath)
        {
            return await _learningPathRepository.CreateLearningPathAsync(learningPath);
        }

        public async Task<string> DeleteLearningPathByIdAsync(string idPath)
        {
            return await _learningPathRepository.DeleteLearningPathByIdAsync(idPath);
        }

        public async Task<LearningPath> GetLearningPathByIdAsync(string idPath)
        {
            return await _learningPathRepository.GetLearningPathsByIdAsync(idPath);
        }

        public async Task<List<LearningPath>> GetLearningPathsAsync()
        {
            return await _learningPathRepository.GetLearningPathsAsync();
        }

        public async Task<List<LearningPath>> GetLearningPathsByCoachAsync(string coachID)
        {
            return await _learningPathRepository.GetLearningPathsByCoachAsync(coachID);
        }

        public async Task<List<LearningPath>> GetLearningPathsByTraineeAsync(string traineeID)
        {
            return await _learningPathRepository.GetLearningPathsByTraineeAsync(traineeID);
        }

        public async Task<LearningPath> UpdateLearningPathByIdAsync(string idPath, UpdateLearningPaths path)
        {
            return await _learningPathRepository.UpdateLearningPathByIdAsync(idPath, path);
        }

        public async Task<string> UpdateLearningPathDurationAsync(string idPath, decimal totalDuration)
        {
            return await _learningPathRepository.UpdateLearningPathDurationAsync(idPath, totalDuration);
        }
    }
}
